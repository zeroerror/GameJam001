using UnityEngine;

public class BulletDomain {

    MainContext mainContext;
    Factory factory;
    BulletFSMDomain bulletFSMDomain;

    public void Inject(MainContext mainContext, Factory factory, BulletFSMDomain bulletFSMDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.bulletFSMDomain = bulletFSMDomain;
    }

    public bool TrySpawnBullet(BulletModel bulletModel,
                               out BulletEntity bullet) {
        if (!factory.TryCreateBullet(bulletModel,
                                     out bullet)) {
            Debug.LogError("TrySpawnBullet failed");
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickBulletID();
        bullet.IDCom.SetEntityID(id);

        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.TryAdd(bullet);

        // PHX
        bullet.OnTriggerEnter += HandleTriggerEnter;
        bullet.OnTriggerExit += HandleTriggerExit;
        return true;
    }

    public void EasingRenderer(float dt) {
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.ForeachAll((bullet) => {
            var fsmCom = bullet.FSMCom;
            if (fsmCom.State != BulletFSMState.None) {
                EasingRenderer(bullet, dt);
            }
        });
    }

    public void EasingRenderer(BulletEntity bullet, float dt) {
        bullet.EasingToDstPos(dt);
    }

    public void HandleHitMonster(in EntityIDArgs bullet, in EntityIDArgs monster) {
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        if (!bulletRepo.TryGet(bullet.entityID, out var bulletEntity)) {
            Debug.LogError($"子弹打击失败 不存在 {bullet}");
            return;
        }

        var fsmCom = bulletEntity.FSMCom;
        var state = fsmCom.State;
        if (state == BulletFSMState.Exploding) {
            Debug.LogWarning($"子弹打击失败 子弹已经爆炸 {bullet}");
            return;
        }

        var monsterRepo = mainContext.rootRepo.monsterRepo;
        if (!monsterRepo.TryGet(monster.entityID, out var monsterEntity)) {
            Debug.LogError($"子弹打击失败 不存在 {monster}");
            return;
        }

        var monsterFSMCom = monsterEntity.FSMCom;
        var monsterState = monsterFSMCom.State;
        if (monsterState == MonsterFSMState.Dying) {
            Debug.LogWarning($"子弹打击失败 怪物已经死亡 {monster}");
            return;
        }

        bulletFSMDomain.Enter_Exploding(bulletEntity);
    }

    void HandleTriggerEnter(EntityIDArgs one, EntityIDArgs two) {
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.TryAdd(one, two);
    }

    void HandleTriggerExit(EntityIDArgs one, EntityIDArgs two) {
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.TryAdd(one, two);
    }


}