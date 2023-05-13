using UnityEngine;

public class BulletDomain {

    MainContext mainContext;
    Factory factory;

    public void Inject(MainContext mainContext, Factory factory) {
        this.mainContext = mainContext;
        this.factory = factory;
    }

    public bool TrySpawnBullet(BulletType bulletType, BulleAttrModel model, out BulletEntity bullet) {
        if (!factory.TryCreateBullet(bulletType, model, out bullet)) {
            Debug.LogError("TrySpawnBullet failed");
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickBulletID();
        bullet.IDCom.SetEntityID(id);
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.TryAdd(bullet);
        return true;
    }

    public void EasingRenderer(float dt) {
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.ForeachAll((bullet) => {
            EasingRenderer(bullet, dt);
        });
    }

    public void EasingRenderer(BulletEntity bullet, float dt) {
        bullet.EasingToDstPos(dt);
    }

}