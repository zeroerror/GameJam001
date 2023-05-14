using UnityEngine;

public class RoleFSMDomain {

    MainContext mainContext;
    RoleDomain roleDomain;
    WeaponFormDomain weaponFormDomain;

    public void Inject(MainContext mainContext, RoleDomain roleDomain, WeaponFormDomain weaponFormFSMDomain) {
        this.mainContext = mainContext;
        this.roleDomain = roleDomain;
        this.weaponFormDomain = weaponFormFSMDomain;
    }

    public void TickFSM(float dt) {
        var roleRepo = mainContext.rootRepo.roleRepo;
        roleRepo.ForeachAll((role) => {
            TickFSM(role, dt);
        });
    }

    void TickFSM(RoleEntity role, float dt) {
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

            role.StopMove_Hor();
        }

        roleDomain.TryShoot(role);

        // ================== EXIT CHECK
        var inputCom = role.InputCom;
        if (inputCom.MoveHorDir != 0) {
            Enter_Moving(role, inputCom.MoveHorDir);
            return;
        }

        if (inputCom.InputJump) {
            Enter_Jumping(role);
            return;
        }
    }

    public void TickMoving(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var model = fsmCom.MovingStateModel;
        var inputCom = role.InputCom;

        var moveHorDir = 0;
        if (model.IsEntering) {
            model.SetIsEntering(false);
            moveHorDir = model.HorDir;
        } else {
            moveHorDir = inputCom.MoveHorDir;
        }

        bool hasMoveDir = moveHorDir != 0;
        if (hasMoveDir) {
            role.SetMoveVelocity(moveHorDir, dt);

            // WeaponFrorm Check
            var weaponForm1 = mainContext.rootRepo.weaponForm1;
            var weaponForm2 = mainContext.rootRepo.weaponForm2;
            var weaponForm3 = mainContext.rootRepo.weaponForm3;
            var x1 = role.LogicPos.x;
            var border1 = 40 / 3f - 20;
            var border2 = (40 * 2 / 3f) - 20;
            if (x1 < border1) {
                weaponFormDomain.Enter(weaponForm1.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm2.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm3.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
            } else if (border1 <= x1 && x1 < border2) {
                weaponFormDomain.Enter(weaponForm2.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm1.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm3.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
            } else {
                weaponFormDomain.Enter(weaponForm3.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm1.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
                weaponFormDomain.Exit(weaponForm2.IDCom.ToEntityIDArgs(), role.IDCom.ToEntityIDArgs());
            }

        }

        roleDomain.TryShoot(role);

        // ================== Exit 
        if (!hasMoveDir) {
            Enter_Idle(role);
        }

        if (inputCom.InputJump) {
            Enter_Jumping(role);
            return;
        }
    }

    public void TickJump(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var model = fsmCom.JumpingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
            role.Jump();
        }

        // Move in the air
        var inputCom = role.InputCom;
        if (inputCom.MoveHorDir != 0) {
            role.SetMoveVelocity(inputCom.MoveHorDir, dt);
        }

        roleDomain.TryShoot(role);

        // ================== Exit 
        bool isGounded = true;
        if (!isGounded) {
            return;
        }

        if (inputCom.MoveHorDir != 0) {
            Enter_Moving(role, inputCom.MoveHorDir);
            return;
        }

        Enter_Idle(role);
        return;
    }

    public void TickAttacking(RoleEntity role, float dt) {
        var fsmCom = role.FSMCom;
        var model = fsmCom.AttackingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // ================== Exit 
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

    public void Enter_Jumping(RoleEntity role) {
        // var fsmCom = role.FSMCom;
        // fsmCom.EnterJumping();
        // Debug.Log("RoleFSM: ======> Enter_Jumping");
    }

}