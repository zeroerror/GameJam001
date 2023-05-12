public class RootDomain {

    public RoleDomain roleDomain;
    public MonsterDomain monsterDomain;
    public BulletDomain bulletDomain;

    public MainContext mainContext;

    public RootDomain(MainContext mainContext) {
        this.mainContext = mainContext;
        this.roleDomain = new RoleDomain(mainContext);
        this.bulletDomain = new BulletDomain(mainContext);
        this.monsterDomain = new MonsterDomain(mainContext);
    }

}