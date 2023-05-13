using UnityEngine;

public class PhxDomain {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public void Tick(float dt) {
        Physics2D.Simulate(dt);
    }

    public void SetGlobalGravity(Vector2 gravity) {
        Physics2D.gravity = gravity;
    }

}