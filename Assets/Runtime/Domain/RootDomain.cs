public class RootDomain {

    public RoleDomain roleDomain;
    public RoleFSMDomain roleFSMDomain;
    public MonsterDomain monsterDomain;
    public BulletDomain bulletDomain;
    public BulletFSMDomain bulletFSMDomain;
    public GameFSMDomain gameFSMDomain;
    public PhxDomain phxDomain;

    public MainContext mainContext;

    public WeaponFormDomain weaponFormDomain;

    public RootDomain() {
        this.roleFSMDomain = new RoleFSMDomain();
        this.roleDomain = new RoleDomain();
        this.bulletDomain = new BulletDomain();
        this.bulletFSMDomain = new BulletFSMDomain();
        this.monsterDomain = new MonsterDomain();
        this.gameFSMDomain = new GameFSMDomain();
        this.phxDomain = new PhxDomain();
        this.weaponFormDomain = new WeaponFormDomain();
    }

    public void Inject(MainContext mainContext, Factory factory) {
        this.mainContext = mainContext;

        this.roleFSMDomain.Inject(mainContext, roleDomain);
        this.roleDomain.Inject(mainContext, factory, roleFSMDomain, bulletFSMDomain, weaponFormDomain);
        this.bulletDomain.Inject(mainContext, factory);
        this.bulletFSMDomain.Inject(mainContext);
        this.monsterDomain.Inject(mainContext);
        this.gameFSMDomain.Inject(mainContext, weaponFormDomain, this);
        this.phxDomain.Inject(mainContext);
        this.weaponFormDomain.Inject(mainContext, factory, bulletDomain);
    }

}