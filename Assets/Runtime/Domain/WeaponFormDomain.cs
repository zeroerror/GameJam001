using UnityEngine;

public class WeaponFormDomain {

    MainContext mainContext;
    Factory factory;
    BulletDomain bulletDomain;

    public void Inject(MainContext mainContext, Factory factory, BulletDomain bulletDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.bulletDomain = bulletDomain;
    }

    public bool TryGetBulletFromWeaponForm_1(out BulletEntity bullet) {
        return TryGetBulletFromWeaponForm(1, out bullet);
    }

    public bool TryGetBulletFromWeaponForm_2(out BulletEntity bullet) {
        return TryGetBulletFromWeaponForm(2, out bullet);
    }

    public bool TryGetBulletFromWeaponForm_3(out BulletEntity bullet) {
        return TryGetBulletFromWeaponForm(3, out bullet);
    }

    public bool TryGetBulletFromWeaponForm(int index, out BulletEntity bullet) {
        var repo = mainContext.rootRepo;
        WeaponFormEntity weaponForm = null;
        if (index == 1) {
            weaponForm = repo.weaponForm1;
        } else if (index == 2) {
            weaponForm = repo.weaponForm2;
        } else if (index == 3) {
            weaponForm = repo.weaponForm3;
        } else {
            Debug.LogError("index error");
        }

        var infoModel = weaponForm.InfoModel;
        var bulletType = weaponForm.BulletType;
        var bulletAttrModel = infoModel.BulleAttrModel;
        return bulletDomain.TrySpawnBullet(bulletType, bulletAttrModel, out bullet);
    }

}