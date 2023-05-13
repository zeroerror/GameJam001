using UnityEngine;

public class RoleDomain {

    MainContext mainContext;
    Factory factory;
    RoleFSMDomain roleFSMDomain;
    WeaponFormDomain weaponFormDomain;
    BulletFSMDomain bulletFSMDomain;

    public void Inject(MainContext mainContext,
                       Factory factory,
                       RoleFSMDomain roleFSMDomain,
                       BulletFSMDomain bulletFSMDomain,
                       WeaponFormDomain weaponFormDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.bulletFSMDomain = bulletFSMDomain;
        this.roleFSMDomain = roleFSMDomain;
        this.weaponFormDomain = weaponFormDomain;
    }

    public void PlayerRole_BackInput() {
        var roleRepo = mainContext.rootRepo.roleRepo;
        var playerRole = roleRepo.PlayerRole;
        if (playerRole == null) {
            return;
        }

        var inputGetter = mainContext.freeInputCore.Getter;
        var inputCom = playerRole.InputCom;

        int moveHorDir = 0;
        bool hasMoveHorDir = false;
        if (inputGetter.GetPressing(InputKeyCollection.MOVE_LEFT)) {
            moveHorDir -= 1;
            hasMoveHorDir = true;
        }
        if (inputGetter.GetPressing(InputKeyCollection.MOVE_RIGHT)) {
            moveHorDir += 1;
            hasMoveHorDir = true;
        }
        if (hasMoveHorDir) {
            inputCom.SetMoveHorDir(moveHorDir);
        }

        if (inputGetter.GetDown(InputKeyCollection.JUMP)) {
            inputCom.SetInputJump(true);
        }

        if (inputGetter.GetDown(InputKeyCollection.PICK)) {
            inputCom.SetInputPick(true);
        }

        if (inputGetter.GetPressing(InputKeyCollection.SHOOT)) {
            inputCom.SetInputShoot(true);
        }

        var pointerPos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        pointerPos.z = 0;
        inputCom.SetChosenPoint(pointerPos);
        Debug.DrawLine(Vector3.zero, pointerPos, Color.red);
    }

    public bool TrySpawnPlayerRole() {
        var roleRepo = mainContext.rootRepo.roleRepo;
        if (roleRepo.PlayerRole != null) {
            Debug.LogError("玩家角色已经存在");
            return false;
        }

        if (!TrySpawnRole(GlobalCollection.PLAYER_TYPE_ID, out var role)) {
            Debug.LogError("创建玩家角色失败");
            return false;
        }

        role.SetPos(new Vector2(0, 0));

        roleRepo.SetPlayerRole(role);
        role.SetDontDestroyOnLoad();

        roleFSMDomain.Enter_Idle(role);

        Debug.Log("创建玩家角色成功");

        return true;
    }

    public bool TrySpawnRole(int typeID, out RoleEntity role) {
        if (!factory.TryCreateRole(typeID, out role)) {
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickRoleID();
        var idCom = role.IDCom;
        idCom.SetEntityID(id);

        var roleRepo = mainContext.rootRepo.roleRepo;
        roleRepo.TryAdd(typeID, role);

        return true;
    }

    public void EasingRenderer(float dt) {
        var roleRepo = mainContext.rootRepo.roleRepo;
        roleRepo.ForeachAll((role) => {
            role.EasingToDstPos(dt);
        });
    }

    public void PlayerRole_AnimWeaponToPos() {
        var roleRepo = mainContext.rootRepo.roleRepo;
        var playerRole = roleRepo.PlayerRole;
        if (playerRole == null) {
            return;
        }

        AnimWeaponToPos(playerRole);
    }

    public void AnimWeaponToPos(RoleEntity role) {
        var inputCom = role.InputCom;
        var chosenPoint = inputCom.ChosenPoint;
        role.AnimWeaponToPos(chosenPoint);
    }

    public bool TryShoot(RoleEntity role) {
        var inputCom = role.InputCom;
        if (!inputCom.InputShoot) {
            return false;
        }

        // Check WeaponForm Connection & Get Bullet
        bool hasShoot = false;
        var shootFromPos = role.LogicPos;
        var endPos = inputCom.ChosenPoint;
        var flyDir = (endPos - shootFromPos).normalized;
        var weaponFormSlotCom = role.WeaponFormSlotCom;
        // temp this
        if (true) {
            if (weaponFormDomain.TryGetBulletFromWeaponForm_1(out var bullet)) {
                bullet.IDCom.SetFather(role.IDCom.ToEntityIDArgs());
                bulletFSMDomain.Enter_Flying(bullet, flyDir);
                hasShoot = true;
                Debug.Log($"Shoot The Bullet From WeaponForm 1");
            }
        }
        if (weaponFormSlotCom.isConnectedToWeaponForm2) {
            if (weaponFormDomain.TryGetBulletFromWeaponForm_2(out var bullet)) {
                // Shoot The Bullet
                bulletFSMDomain.Enter_Flying(bullet, flyDir);
                hasShoot = true;
                Debug.Log($"Shoot The Bullet From WeaponForm 2");
            }
        }
        if (weaponFormSlotCom.isConnectedToWeaponForm3) {
            if (weaponFormDomain.TryGetBulletFromWeaponForm_3(out var bullet)) {
                // Shoot The Bullet
                bulletFSMDomain.Enter_Flying(bullet, flyDir);
                hasShoot = true;
                Debug.Log($"Shoot The Bullet From WeaponForm 3");
            }
        }

        return hasShoot;
    }

}