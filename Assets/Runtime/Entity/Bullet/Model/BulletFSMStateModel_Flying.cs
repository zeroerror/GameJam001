using UnityEngine;

public class BulletFSMStateModel_Flying {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    Vector2 flyDir;
    public Vector2 FlyDir => flyDir;
    public void SetFlyDir(Vector2 value) => flyDir = value;

    public float time;

    public BulletFSMStateModel_Flying() { }

    public void Reset() {
        isEntering = false;
        isExiting = false;
        flyDir = Vector2.zero;
        time = 0f;
    }

}