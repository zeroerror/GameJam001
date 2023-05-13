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

    public void TrySpawnWeaponFormThree() {
        var rootRepo = mainContext.rootRepo;
        if (rootRepo.weaponForm1 != null
        || rootRepo.weaponForm2 != null
        || rootRepo.weaponForm3 != null) {
            Debug.LogError("WeaponForm Already Spawned");
            return;
        }

        TrySpawnWeaponForm(1);
        TrySpawnWeaponForm(2);
        TrySpawnWeaponForm(3);
    }

    public bool TrySpawnWeaponForm(int index) {
        var str = "WeaponForm/go_template_weaponform";
        var prefab = Resources.Load(str);
        if (prefab == null) {
            Debug.LogError($"TrySpawnWeaponForm {str} Not Found");
            return false;
        }

        var go = GameObject.Instantiate(prefab) as GameObject;
        var weaponForm = new WeaponFormEntity();
        weaponForm.Inject(go);
        var rootRepo = mainContext.rootRepo;
        if (index == 1) {
            rootRepo.weaponForm1 = weaponForm;
        } else if (index == 2) {
            rootRepo.weaponForm2 = weaponForm;
        } else if (index == 3) {
            rootRepo.weaponForm3 = weaponForm;
        }

        return true;
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
        if (bulletDomain.TrySpawnBullet(bulletType, bulletAttrModel, out bullet)) {
            bullet.SetFlySpeed(10);
            return true;
        }

        return true;
    }

}