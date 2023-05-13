using UnityEngine;

public class BulletFSMDomain {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public void TickFSM(float dt) {
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.ForeachAll((bullet) => {
            TickFSM(bullet, dt);
        });
    }

    void TickFSM(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var state = fsmCom.State;
        if (state == BulletFSMState.None) {
            return;
        }

        if (state == BulletFSMState.Flying) {
            TickFlying(bullet, dt);
        } else if (state == BulletFSMState.Exploding) {
            TickExploding(bullet, dt);
        }

        TickAny(bullet, dt);
    }

    public void TickAny(BulletEntity bullet, float dt) {
    }

    public void TickFlying(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var model = fsmCom.FlyingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // Flyinggggggggggggggggggggg
    }

    public void TickExploding(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var model = fsmCom.ExplodingStateModel;

        // ================== Exit 
    }

    public void Enter_Flying(BulletEntity bullet, Vector2 dir) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterFlying();
        Debug.Log($"RoleFSM: ======> Enter_Flying dir:{dir}");
    }

    public void Exploding(BulletEntity bullet) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterExploding();
        Debug.Log("RoleFSM: ======> Enter_Exploding");
    }


}