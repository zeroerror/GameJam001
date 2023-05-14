public class BulletFSMStateModel_Exploding {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    float explodeRadius;
    public float ExplodeRadius => explodeRadius;
    public void SetExplodeRadius(float value) => explodeRadius = value;

    public BulletFSMStateModel_Exploding() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
        explodeRadius = 0f;
    }

}