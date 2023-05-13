using UnityEngine;

public class MonsterDomain {

    MainContext mainContext;
    Factory factory;
    MonsterFSMDomain monsterFSMDomain;

    public void Inject(MainContext mainContext, Factory factory, MonsterFSMDomain monsterFSMDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.monsterFSMDomain = monsterFSMDomain;
    }

    public bool TrySpawnMonster(WaveSpawnerModel spawnerModel, out MonsterEntity monster) {
        var typeID = spawnerModel.typeID;
        if (!factory.TryCreateMonster(typeID, out monster)) {
            Debug.LogError($"创建怪物失败 {typeID}");
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickMonsterID();
        monster.IDCom.SetEntityID(id);

        monsterFSMDomain.Enter_Falling(monster);

        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var xRange = globalConfigTM.monsterSpawnPosXRange;
        var posX = Random.Range(xRange.x, xRange.y);
        var posY = globalConfigTM.monsterSpawnPosY;
        monster.SetPos(new Vector2(posX, posY));

        var monsterRepo = mainContext.rootRepo.monsterRepo;
        monsterRepo.TryAdd(monster);

        return true;
    }

    public void EasingRenderer(float dt) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        monsterRepo.ForeachAll((monster) => {
            if (monster.FSMCom.State != MonsterFSMState.None) {
                monster.EasingToDstPos(dt);
            }
        });
    }

    public void HandleBeHitByBullet(in EntityIDArgs monster, in EntityIDArgs bullet) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        if (!monsterRepo.TryGet(monster.entityID, out var monsterEntity)) {
            Debug.LogError($"怪物打击失败 不存在 ");
            return;
        }

        var fsmCom = monsterEntity.FSMCom;
        var state = fsmCom.State;
        if (state == MonsterFSMState.Dying) {
            return;
        }

        var bulletRepo = mainContext.rootRepo.bulletRepo;
        if (!bulletRepo.TryGet(bullet.entityID, out var bulletEntity)) {
            Debug.LogError($"怪物打击失败 子弹不存在 ");
            return;
        }

        var damage = bulletEntity.bulletDamage;
        var clampHP = System.Math.Clamp(monsterEntity.HP - damage, 0, int.MaxValue);
        monsterEntity.SetHP(clampHP);
        Debug.Log($"ZZZ clampHP:{clampHP} damage:{damage}");

    }

    public void HandleHitWeaponForm(in EntityIDArgs monster, in EntityIDArgs weaponForm) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        if (!monsterRepo.TryGet(monster.entityID, out var monsterEntity)) {
            Debug.LogError($"怪物打击失败 不存在 ");
            return;
        }

        var fsmCom = monsterEntity.FSMCom;
        var state = fsmCom.State;
        if (state == MonsterFSMState.Dying) {
            return;
        }

        monsterFSMDomain.Enter_Dying(monsterEntity);
    }

}