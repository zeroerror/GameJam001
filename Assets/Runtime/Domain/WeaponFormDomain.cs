using UnityEngine;

public class WeaponFormDomain {

    MainContext mainContext;
    Factory factory;
    BulletDomain bulletDomain;
    BulletFSMDomain bulletFSMDomain;
    WeaponFormFSMDomain weaponFormFSMDomain;
    PhxDomain phxDomain;

    public void Inject(MainContext mainContext,
                       Factory factory,
                       BulletDomain bulletDomain,
                       BulletFSMDomain bulletFSMDomain,
                       WeaponFormFSMDomain weaponFormFSMDomain,
                       PhxDomain phxDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.bulletDomain = bulletDomain;
        this.bulletFSMDomain = bulletFSMDomain;
        this.weaponFormFSMDomain = weaponFormFSMDomain;
        this.phxDomain = phxDomain;
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
        TrySpawnWeaponForm(1, pos1, out var weaponForm1);
        TrySpawnWeaponForm(2, pos2, out var weaponForm2);
        TrySpawnWeaponForm(3, pos3, out var weaponForm3);

        // weaponForm1.roleEntity = rootRepo.roleRepo.PlayerRole;
        // weaponForm2.roleEntity = rootRepo.roleRepo.PlayerRole;
        // weaponForm3.roleEntity = rootRepo.roleRepo.PlayerRole;

        // PHX
        var weaponFormChildren1 = weaponForm1.weaponFormChildren;
        for (var i = 0; i < weaponForm1.weaponFormChildrenCount; i++) {
            var weaponFormChild = weaponFormChildren1[i];
            weaponFormChild.OnTriggerEnter += phxDomain.HandleTriggerEnter;
            weaponFormChild.OnTriggerExit += phxDomain.HandleTriggerExit;
        }
        var weaponFormChildren2 = weaponForm2.weaponFormChildren;
        for (var i = 0; i < weaponForm1.weaponFormChildrenCount; i++) {
            var weaponFormChild = weaponFormChildren2[i];
            weaponFormChild.OnTriggerEnter += phxDomain.HandleTriggerEnter;
            weaponFormChild.OnTriggerExit += phxDomain.HandleTriggerExit;
        }

        var weaponFormChildren3 = weaponForm3.weaponFormChildren;
        for (var i = 0; i < weaponForm1.weaponFormChildrenCount; i++) {
            var weaponFormChild = weaponFormChildren3[i];
            weaponFormChild.OnTriggerEnter += phxDomain.HandleTriggerEnter;
            weaponFormChild.OnTriggerExit += phxDomain.HandleTriggerExit;
        }
    }

    public bool TrySpawnWeaponForm(int index, Vector2 pos, out WeaponFormEntity weaponForm) {
        weaponForm = null;

        var str = "WeaponForm/go_template_weaponform";
        var prefab = Resources.Load(str);
        if (prefab == null) {
            Debug.LogError($"TrySpawnWeaponForm {str} Not Found");
            return false;
        }

        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        GameObject.DontDestroyOnLoad(rootGO);

        weaponForm = rootGO.AddComponent<WeaponFormEntity>();
        weaponForm.Ctor();

        weaponForm.Inject(rootGO);
        weaponForm.SetPos(pos);

        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var attrModel = TM2ModelUtil.GetWeaponFormAttrModel(globalConfigTM);
        mainContext.rootTemplate.bulletTemplate.TryGet(globalConfigTM.bulletType_init,
                                                       out var bulletTM);
        attrModel.bulletModel = TM2ModelUtil.GetBulletModel(bulletTM);
        weaponForm.SetWeaponFormAttrModel(attrModel);
        weaponForm.SetBulletType(globalConfigTM.bulletType_init);
        weaponForm.curBulletCount = attrModel.bulletCapacity;
        weaponForm.IDCom.SetEntityID(index);

        var rootRepo = mainContext.rootRepo;
        if (index == 1) {
            rootRepo.weaponForm1 = weaponForm;
        } else if (index == 2) {
            rootRepo.weaponForm2 = weaponForm;
        } else if (index == 3) {
            rootRepo.weaponForm3 = weaponForm;
        }

        weaponFormFSMDomain.Enter_Idle(weaponForm);

        return true;
    }

    public void Shoot(WeaponFormEntity weaponForm, Vector3 shootTarPos) {
        var role = weaponForm.roleEntity;
        if (role == null) {
            Debug.LogWarning("Shoot: role==null");
            return;
        }

        var attrModel = weaponForm.AttrModel;
        var bulletType = weaponForm.BulletType;
        if (!bulletDomain.TrySpawnBullet(attrModel.bulletModel, out var bullet)) {
            Debug.LogWarning($"WeaponFormFSM: TickShooting: TrySpawnBullet failed");
            return;
        }

        var inputCom = role.InputCom;
        var shootFromPos = role.LogicPos + new Vector3(0, 2, 0);
        var flyDir = (shootTarPos - shootFromPos).normalized;

        bullet.IDCom.SetFather(role.IDCom.ToEntityIDArgs());
        bullet.SetPos(shootFromPos);
        bulletFSMDomain.Enter_Flying(bullet, flyDir);

        weaponForm.curBulletCount--;

        if (bullet.bulletType == BulletType.Rocket) {
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Rocket_Shoot();
        }
    }

    public bool TryShootFromWeaponForm_1(Vector2 shootTarPos, out BulletEntity bullet) {
        return TryShootFromWeaponForm(1, shootTarPos, out bullet);
    }

    public bool TryShootFromWeaponForm_2(Vector2 shootTarPos, out BulletEntity bullet) {
        return TryShootFromWeaponForm(2, shootTarPos, out bullet);
    }

    public bool TryShootFromWeaponForm_3(Vector2 shootTarPos, out BulletEntity bullet) {
        return TryShootFromWeaponForm(3, shootTarPos, out bullet);
    }

    public bool TryShootFromWeaponForm(int index, Vector2 shootTarPos, out BulletEntity bullet) {
        bullet = null;

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

        if (weaponForm.FSMCom.State != WeaponFormFSMState.Idle
        && weaponForm.FSMCom.State != WeaponFormFSMState.Reloading
        ) {
            return false;
        }

        if (weaponForm.roleEntity == null) {
            return false;
        }

        weaponFormFSMDomain.Enter_Shooting(weaponForm, shootTarPos);
        return true;
    }

    public void HandleBeHitByMonster(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs monsterIDArgs) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        if (!monsterRepo.TryGet(monsterIDArgs.entityID, out var monsterEntity)) {
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

        var gameEntity = mainContext.GameEntity;
        var hp = gameEntity.baseHP;

        if (idCom1.EntityID == entityID) {
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            var damage = hp - clampHP;
            Debug.Log($"武器库 1 受到怪物{monsterIDArgs}攻击 受到伤害{damage} 剩余血量{clampHP}");
            gameEntity.baseHP = clampHP;
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Normal();
            return;
        }

        if (idCom2.EntityID == entityID) {
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            var damage = hp - clampHP;
            Debug.Log($"武器库 2 受到怪物{monsterIDArgs}攻击 受到伤害{damage} 剩余血量{clampHP}");
            gameEntity.baseHP = clampHP;
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Normal();
            return;
        }

        if (idCom3.EntityID == entityID) {
            var clampHP = System.Math.Clamp(hp - 1, 0, int.MaxValue);
            var damage = hp - clampHP;
            Debug.Log($"武器库 3 受到怪物{monsterIDArgs}攻击 受到伤害{damage} 剩余血量{clampHP}");
            gameEntity.baseHP = clampHP;
            var camMgr = mainContext.CameraManager;
            camMgr.Shake_Normal();
            return;
        }

        Debug.LogError($"怪物打击武器库失败 不存在 {weaponFormIDArgs}");
    }

    public void HandleHitRole(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs roleIDArgs) {
        return;



        Debug.LogError($"怪物打击武器库失败 不存在 {weaponFormIDArgs}");
    }

    public void Enter(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs roleIDArgs) {
        var roleRepo = mainContext.rootRepo.roleRepo;
        if (!roleRepo.TryGet(roleIDArgs.entityID, out var roleEntity)) {
            Debug.LogError($"角色 链接====》 武器库失败 不存在 {roleIDArgs}");
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
            Debug.Log($"武器库 1 链接 =========================");
            weaponForm1.roleEntity = roleEntity;
            var bulletModel = weaponForm1.AttrModel.bulletModel;
            roleEntity.SetWeaponSprite(bulletModel.weaponIcon, bulletModel.themeColor);
            return;
        }

        if (idCom2.EntityID == entityID) {
            Debug.Log($"武器库 2 链接 =========================");
            weaponForm2.roleEntity = roleEntity;
            var bulletModel = weaponForm2.AttrModel.bulletModel;
            roleEntity.SetWeaponSprite(bulletModel.weaponIcon, bulletModel.themeColor);
            return;
        }

        if (idCom3.EntityID == entityID) {
            Debug.Log($"武器库 3 链接 =========================");
            weaponForm3.roleEntity = roleEntity;
            var bulletModel = weaponForm3.AttrModel.bulletModel;
            roleEntity.SetWeaponSprite(bulletModel.weaponIcon, bulletModel.themeColor);
            return;
        }
    }

    public void HandleExitRole(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs roleIDArgs) {
        return;

    }

    public void Exit(in EntityIDArgs weaponFormIDArgs, in EntityIDArgs roleIDArgs) {

        var roleRepo = mainContext.rootRepo.roleRepo;
        if (!roleRepo.TryGet(roleIDArgs.entityID, out var roleEntity)) {
            Debug.LogError($"角色打击武器库失败 不存在 {roleIDArgs}");
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
            Debug.Log($"武器库 1 取消链接 ==========================");
            weaponForm1.roleEntity = null;
            return;
        }

        if (idCom2.EntityID == entityID) {
            Debug.Log($"武器库 2 取消链接 ==========================");
            weaponForm2.roleEntity = null;
            return;
        }

        if (idCom3.EntityID == entityID) {
            Debug.Log($"武器库 3 取消链接 ==========================");
            weaponForm3.roleEntity = null;
            return;
        }

        Debug.LogError($"怪物打击武器库失败 不存在 {weaponFormIDArgs}");
    }

}