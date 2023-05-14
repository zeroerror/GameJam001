using UnityEngine;

public class MonsterDomain {

    MainContext mainContext;
    Factory factory;
    MonsterFSMDomain monsterFSMDomain;
    PhxDomain phxDomain;

    public void Inject(MainContext mainContext, Factory factory, MonsterFSMDomain monsterFSMDomain, PhxDomain phxDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.monsterFSMDomain = monsterFSMDomain;
        this.phxDomain = phxDomain;
    }

    public bool TrySpawnMonster(WaveSpawnerModel spawnerModel, Vector2 rdPos, out MonsterEntity monster) {
        var typeID = spawnerModel.typeID;
        return SpawnMonster(typeID, rdPos, out monster);
    }

    public bool SpawnMonster(int typeID, Vector2 rdPos, out MonsterEntity monster) {
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


        var monsterShield = monster.monsterShield;
        monsterShield.OnTriggerEnter += phxDomain.HandleTriggerEnter;
        monsterShield.OnTriggerExit += phxDomain.HandleTriggerExit;

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

        // shield check
        var monsterShield = monsterEntity.monsterShield;
        bool hasBlock = false;
        monsterShield.ForeachFrameBlockBullets((b) => {
            if (b.IDCom.EntityID == bulletEntity.IDCom.EntityID) {
                hasBlock = true;
            }
        });

        if (hasBlock) {
            Debug.Log($"子弹 被 怪物盾牌 阻挡 ");
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