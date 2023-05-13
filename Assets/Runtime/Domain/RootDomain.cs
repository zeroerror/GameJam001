public class RootDomain {

    public RoleDomain roleDomain;
    public RoleFSMDomain roleFSMDomain;
    public MonsterDomain monsterDomain;
    public BulletDomain bulletDomain;
    public GameFSMDomain gameFSMDomain;
    public PhxDomain phxDomain;

    public MainContext mainContext;

    public RootDomain() {
        this.mainContext = new MainContext();
        this.roleFSMDomain = new RoleFSMDomain();
        this.roleDomain = new RoleDomain();
        this.bulletDomain = new BulletDomain();
        this.monsterDomain = new MonsterDomain();
        this.gameFSMDomain = new GameFSMDomain();
        this.phxDomain = new PhxDomain();
    }

    public void Inject(MainContext mainContext, Factory factory) {
        this.mainContext = mainContext;

        this.roleFSMDomain.Inject(mainContext);
        this.roleDomain.Inject(mainContext, factory, roleFSMDomain);
        this.bulletDomain.Inject(mainContext);
        this.monsterDomain.Inject(mainContext);
        this.gameFSMDomain.Inject(mainContext, this);
        this.phxDomain.Inject(mainContext);
    }

}