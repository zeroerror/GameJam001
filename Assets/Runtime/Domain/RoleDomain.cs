using UnityEngine;

public class RoleDomain {

    MainContext mainContext;
    Factory factory;
    RoleFSMDomain roleFSMDomain;

    public void Inject(MainContext mainContext, Factory factory, RoleFSMDomain roleFSMDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.roleFSMDomain = roleFSMDomain;
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

        // TTTT
        factory.TryCreateBullet(123213211,out var bullet);
        bullet.SetPos(new Vector2(5, 4));

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

    public void PlayerRole_AnimWeaponToPos(){
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

}