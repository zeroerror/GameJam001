using UnityEngine;

public class BulletDomain {

    MainContext mainContext;
    Factory factory;
    BulletFSMDomain bulletFSMDomain;
    PhxDomain phxDomain;

    public void Inject(MainContext mainContext, Factory factory, BulletFSMDomain bulletFSMDomain, PhxDomain phxDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.bulletFSMDomain = bulletFSMDomain;
        this.phxDomain = phxDomain;
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
        bullet.OnTriggerEnter += phxDomain.HandleTriggerEnter;
        bullet.OnTriggerExit += phxDomain.HandleTriggerExit;
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
        
        if (bulletEntity.bulletType == BulletType.Rocket) {
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Rocket_Hit();
        }

        bulletFSMDomain.Enter_Exploding(bulletEntity, 1000f);
    }

    public void HandleHitWall(in EntityIDArgs bullet, Vector2 normal) {
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

        if (bulletEntity.bulletType == BulletType.Bubble) {
            bulletEntity.Bounce(normal);
        }

        if (bulletEntity.bulletType == BulletType.Rocket) {
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Rocket_Hit();
        }

    }

}