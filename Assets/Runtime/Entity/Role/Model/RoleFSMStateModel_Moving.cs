public class RoleFSMStateModel_Moving {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    int horDir;
    public int HorDir => horDir;
    public void SetHorDir(int value) => horDir = value;

    public RoleFSMStateModel_Moving() {}

    public void Reset() {
        isEntering = false;
        isExiting = false;
        horDir = 0;
    }

}