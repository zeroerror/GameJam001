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

    public bool TrySpawnMonster(WaveSpawnerModel spawnerModel, Vector2 rdPos, out MonsterEntity monster) {
        var typeID = spawnerModel.typeID;
        if (!factory.TryCreateMonster(typeID, out monster)) {
            Debug.LogError($"创建怪物失败 {typeID}");
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickMonsterID();
        monster.IDCom.SetEntityID(id);

        monsterFSMDomain.Enter_Falling(monster);

        var posX = rdPos.x + 2 - 20;
        var posY = 22.5f - rdPos.y;
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

        if (bulletEntity.bulletType == BulletType.Rocket) {
            return;
        }

        var damage = bulletEntity.bulletDamage;
        var clampHP = System.Math.Clamp(monsterEntity.HP - damage, 0, int.MaxValue);
        monsterEntity.SetHP(clampHP);

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