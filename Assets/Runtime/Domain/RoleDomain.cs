public class RoleDomain {

    MainContext mainContext;

    public RoleDomain(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TryCreateRole() {
        return false;
    }

}