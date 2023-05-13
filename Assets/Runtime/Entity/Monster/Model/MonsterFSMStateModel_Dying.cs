public class MonsterFSMStateModel_Dying {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    public MonsterFSMStateModel_Dying() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
    }

}