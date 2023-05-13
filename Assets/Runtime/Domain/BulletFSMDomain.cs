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

        var flyDir = model.FlyDir;
        if (model.IsEntering) {
            model.SetIsEntering(false);
            bullet.Fly(flyDir * bullet.FlySpeed);
        }

        model.time += dt;

        // ================== Exit 
        if (model.time > 5f) {
            Enter_Exploding(bullet);
            return;
        }
    }

    public void TickExploding(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var model = fsmCom.ExplodingStateModel;

        // ================== Exit 
    }

    public void Enter_Flying(BulletEntity bullet, Vector2 flyDir) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterFlying(flyDir);
        Debug.Log($"BulletFSM: ======> Enter_Flying dir:{flyDir}");
    }

    public void Enter_Exploding(BulletEntity bullet) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterExploding();
        Debug.Log("BulletFSM: ======> Enter_Exploding");
    }


}