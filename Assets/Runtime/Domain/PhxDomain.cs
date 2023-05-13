using UnityEngine;

public class PhxDomain {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public void Tick(float dt) {
        Physics2D.Simulate(dt);

        // PHX EVENT
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.ForeachAllEnter((evModel) => {

        });

        phxEventRepo.ForeachAllExit((evModel) => {

        });

        phxEventRepo.ClearAll();
    }

    public void SetGlobalGravity(Vector2 gravity) {
        Physics2D.gravity = gravity;
    }

}