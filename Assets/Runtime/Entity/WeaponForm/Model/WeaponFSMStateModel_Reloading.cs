public class WeaponFSMStateModel_Reloading {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    public float time;

    public WeaponFSMStateModel_Reloading() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
        time = 0;
    }

}