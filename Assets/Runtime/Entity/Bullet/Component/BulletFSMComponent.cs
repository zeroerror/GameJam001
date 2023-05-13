using UnityEngine;

public class BulletFSMComponent {

    BulletFSMState state;
    public BulletFSMState State => state;

    BulletFSMStateModel_Flying flyingStateModel;
    public BulletFSMStateModel_Flying FlyingStateModel => flyingStateModel;

    BulletFSMStateModel_Exploding explodingStateModel;
    public BulletFSMStateModel_Exploding ExplodingStateModel => explodingStateModel;

    public BulletFSMComponent() {
        state = BulletFSMState.None;
        flyingStateModel = new BulletFSMStateModel_Flying();
        explodingStateModel = new BulletFSMStateModel_Exploding();
    }

    public void Reset() {
        state = BulletFSMState.None;
        flyingStateModel.Reset();
        explodingStateModel.Reset();
    }

    public void Exit(){
        Reset();
    }

    public void EnterFlying(Vector2 flyDir) {
        var model = flyingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        model.SetFlyDir(flyDir);
        state = BulletFSMState.Flying;
    }

    public void EnterExploding() {
        var model = explodingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        state = BulletFSMState.Exploding;
    }

}