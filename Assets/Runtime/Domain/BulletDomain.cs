using UnityEngine;

public class BulletDomain {

    MainContext mainContext;
    Factory factory;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TrySpawnBullet(BulletType bulletType, BulleAttrModel model, out BulletEntity bullet) {
        if (!factory.TryCreateBullet(bulletType, model, out bullet)) {
            Debug.LogError("TrySpawnBullet failed");
            return false;
        }

        return true;
    }

}