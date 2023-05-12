public class BulletDomain {

    MainContext mainContext;

    public BulletDomain(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TryCreateBullet() {
        return false;
    }

}