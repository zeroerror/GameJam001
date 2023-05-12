public class RoleFSMDomain {

    MainContext mainContext;

    public RoleFSMDomain(MainContext mainContext) {
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
            TickMove(role, dt);
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
    }

    public void TickMove(RoleEntity role, float dt) {
    }

    public void TickJump(RoleEntity role, float dt) {
    }

    public void TickAttacking(RoleEntity role, float dt) {
    }

}