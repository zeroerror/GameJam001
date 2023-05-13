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

        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var pos1 = globalConfigTM.weaponFormPos1;
        var pos2 = globalConfigTM.weaponFormPos2;
        var pos3 = globalConfigTM.weaponFormPos3;
        TrySpawnWeaponForm(1, pos1);
        TrySpawnWeaponForm(2, pos2);
        TrySpawnWeaponForm(3, pos3);
    }

    public bool TrySpawnWeaponForm(int index, Vector2 pos) {
        var str = "WeaponForm/go_template_weaponform";
        var prefab = Resources.Load(str);
        if (prefab == null) {
            Debug.LogError($"TrySpawnWeaponForm {str} Not Found");
            return false;
        }

        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        GameObject.DontDestroyOnLoad(rootGO);
        var weaponForm = new WeaponFormEntity();
        weaponForm.Inject(rootGO);
        weaponForm.SetPos(pos);

        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var weaponFormInitTM = globalConfigTM.weaponFormInitTM;
        var attrModel = TM2ModelUtil.GetWeaponFormAttrModel(weaponFormInitTM);
        attrModel.baseHP = globalConfigTM.baseHP;
        attrModel.hp = globalConfigTM.baseHP;
        weaponForm.SetWeaponFormAttrModel(attrModel);

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

        var attrModel = weaponForm.AttrModel;
        var bulletType = weaponForm.BulletType;
        if (bulletDomain.TrySpawnBullet(bulletType,
                                        attrModel.bulletSize,
                                        attrModel.bloodThirst,
                                        attrModel.fanOut,
                                        attrModel.slow,
                                        attrModel.hitBackDis,
                                        attrModel.bulletDamage,
                                        out bullet)) {
            bullet.SetFlySpeed(10);
            return true;
        }

        return true;
    }

    public void HandleBeHitByMonster(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs monsterIDArgs) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        if (!monsterRepo.TryGet(monsterIDArgs.typeID, out var monsterEntity)) {
            Debug.LogError($"怪物打击武器库失败 不存在 {monsterIDArgs}");
            return;
        }

        var weaponForm1 = mainContext.rootRepo.weaponForm1;
        var weaponForm2 = mainContext.rootRepo.weaponForm2;
        var weaponForm3 = mainContext.rootRepo.weaponForm3;

        var idCom1 = weaponForm1.IDCom;
        var idCom2 = weaponForm2.IDCom;
        var idCom3 = weaponForm3.IDCom;
        var entityID = weaponFormIDArgs.entityID;

        if (idCom1.EntityID == entityID) {
            var hp = weaponForm1.AttrModel.hp;
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            monsterEntity.SetHP(clampHP);
            return;
        }

        if (idCom2.EntityID == entityID) {
            var hp = weaponForm2.AttrModel.hp;
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            monsterEntity.SetHP(clampHP);
            return;
        }

        if (idCom3.EntityID == entityID) {
            var hp = weaponForm3.AttrModel.hp;
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            monsterEntity.SetHP(clampHP);
            return;
        }
    }

}