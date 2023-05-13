public class RootRepo{

    public RoleRepo roleRepo;
    public BulletRepo bulletRepo;
    public MonsterRepo monsterRepo;

    public WeaponFormEntity weaponForm1;
    public WeaponFormEntity weaponForm2;
    public WeaponFormEntity weaponForm3;

    public RootRepo(){
        this.roleRepo = new RoleRepo();
        this.bulletRepo = new BulletRepo();
        this.monsterRepo = new MonsterRepo();
    }

}