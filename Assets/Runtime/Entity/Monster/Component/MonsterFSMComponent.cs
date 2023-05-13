public class MonsterFSMComponent {

    MonsterFSMState state;
    public MonsterFSMState State => state;

    MonsterFSMStateModel_Falling fallingStateModel;
    public MonsterFSMStateModel_Falling FallingStateModel => fallingStateModel;

    MonsterFSMStateModel_Dying dyingStateModel;
    public MonsterFSMStateModel_Dying DyingStateModel => dyingStateModel;

    public MonsterFSMComponent() {
        state = MonsterFSMState.None;
        fallingStateModel = new MonsterFSMStateModel_Falling();
        dyingStateModel = new MonsterFSMStateModel_Dying();
    }

    public void Reset() {
        state = MonsterFSMState.None;
        fallingStateModel.Reset();
    }

    public void Exit() {
        Reset();
    }

    public void EnterFalling() {
        var model = fallingStateModel;
        model.Reset();
        model.SetIsEntering(true);

        state = MonsterFSMState.Falling;
    }

    public void EnterDying() {
        var model = dyingStateModel;
        model.Reset();
        model.SetIsEntering(true);

        state = MonsterFSMState.Dying;
    }

}