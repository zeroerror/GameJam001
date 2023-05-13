using UnityEngine;

public class RoleFSMComponent {

    RoleFSMState state;
    public RoleFSMState State => state;

    RoleFSMStateModel_Idle idleStateModel;
    public RoleFSMStateModel_Idle IdleStateModel => idleStateModel;

    RoleFSMStateModel_Moving movingStateModel;
    public RoleFSMStateModel_Moving MovingStateModel => movingStateModel;

    RoleFSMStateModel_Jumping jumpingStateModel;
    public RoleFSMStateModel_Jumping JumpingStateModel => jumpingStateModel;

    RoleFSMStateModel_Attacking attackingStateModel;
    public RoleFSMStateModel_Attacking AttackingStateModel => attackingStateModel;


    public RoleFSMComponent() {
        state = RoleFSMState.None;
        idleStateModel = new RoleFSMStateModel_Idle();
        movingStateModel = new RoleFSMStateModel_Moving();
        jumpingStateModel = new RoleFSMStateModel_Jumping();
        attackingStateModel = new RoleFSMStateModel_Attacking();
    }

    public void Reset() {
        state = RoleFSMState.None;
        idleStateModel.Reset();
        movingStateModel.Reset();
        jumpingStateModel.Reset();
        attackingStateModel.Reset();
    }

    public void EnterIdle() {
        var model = idleStateModel;
        model.Reset();
        model.SetIsEntering(true);

        state = RoleFSMState.Idle;
    }

    public void EnterMoving(int horDir) {
        var model = movingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        model.SetHorDir(horDir);

        state = RoleFSMState.Moving;
    }

    public void EnterJumping() {
        var model = jumpingStateModel;
        model.Reset();
        model.SetIsEntering(true);

        state = RoleFSMState.Jumping;
    }

    public void EnterAttacking() {
        var model = attackingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        
        state = RoleFSMState.Attacking;
    }

}