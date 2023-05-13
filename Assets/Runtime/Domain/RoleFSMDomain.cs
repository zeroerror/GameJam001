using UnityEngine;

public class RoleFSMDomain {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public void TickFSM(float dt) {
        var roleRepo = mainContext.rootRepo.roleRepo;
        roleRepo.ForeachAll((role) => {
            TickFSW(role, dt);
        });
    }

    void TickFSW(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var state = fsmCom.State;
        if (state == RoleFSMState.None) {
            return;
        }

        if (state == RoleFSMState.Idle) {
            TickIdle(role, dt);
        } else if (state == RoleFSMState.Moving) {
            TickMoving(role, dt);
        } else if (state == RoleFSMState.Jumping) {
            TickJump(role, dt);
        } else if (state == RoleFSMState.Attacking) {
            TickAttacking(role, dt);
        }

        TickAny(role, dt);
    }

    public void TickAny(RoleEntity role, float dt) {
        var inputCom = role.InputCom;
        inputCom.Reset();
    }

    public void TickIdle(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var model = fsmCom.IdleStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        var inputCom = role.InputCom;
        if (inputCom.MoveHorDir != 0) {
            Enter_Moving(role, inputCom.MoveHorDir);
        }
    }

    public void TickMoving(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var model = fsmCom.MovingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        role.Move_Hor(model.HorDir, dt);

        var inputCom = role.InputCom;
        if (inputCom.MoveHorDir == 0) {
            Enter_Idle(role);
        }

        model.SetHorDir(inputCom.MoveHorDir);
    }

    public void TickJump(RoleEntity role, float dt) {
    }

    public void TickAttacking(RoleEntity role, float dt) {
    }

    public void Enter_Idle(RoleEntity role) {
        var fsmCom = role.FSMCom;
        fsmCom.EnterIdle();
        Debug.Log("RoleFSM: ======> Enter_Idle");
    }

    public void Enter_Moving(RoleEntity role, int horDir) {
        var fsmCom = role.FSMCom;
        fsmCom.EnterMoving(horDir);
        Debug.Log("RoleFSM: ======> Enter_Moving");
    }

}