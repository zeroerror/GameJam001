public class BulletFSMStateModel_Exploding {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    public BulletFSMStateModel_Exploding() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
    }

}