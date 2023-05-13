public class RoleFSMStateModel_Idle {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    public RoleFSMStateModel_Idle() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
    }

}