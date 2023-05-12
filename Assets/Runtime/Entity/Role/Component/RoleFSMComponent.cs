using UnityEngine;

public class RoleFSMComponent {

    RoleFSMState state;
    public RoleFSMState State => state;

    public RoleFSMComponent() { 
        state = RoleFSMState.None;
    }

    public void Reset() {
    }

    public void EnterIdle() {
        state = RoleFSMState.Idle;
    }

    public void EnterMoving() {
        state = RoleFSMState.Moving;
    }

    public void EnterJumping() {
        state = RoleFSMState.Jumping;
    }

    public void EnterAttacking() {
        state = RoleFSMState.Attacking;
    }

}