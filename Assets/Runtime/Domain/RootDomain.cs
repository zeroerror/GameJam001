public class RootDomain {

    public RoleDomain roleDomain;
    public RoleFSMDomain roleFSMDomain;
    public MonsterDomain monsterDomain;
    public BulletDomain bulletDomain;

    public MainContext mainContext;

    public RootDomain(MainContext mainContext, Factory factory) {
        this.mainContext = mainContext;
        this.roleDomain = new RoleDomain(mainContext, factory);
        this.roleFSMDomain = new RoleFSMDomain(mainContext);
        this.bulletDomain = new BulletDomain(mainContext);
        this.monsterDomain = new MonsterDomain(mainContext);
    }

}