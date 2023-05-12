public class RootRepo{

    public RoleRepo roleRepo;
    public BulletRepo bulletRepo;
    public MonsterRepo monsterRepo;

    public RootRepo(){
        this.roleRepo = new RoleRepo();
        this.bulletRepo = new BulletRepo();
        this.monsterRepo = new MonsterRepo();
    }

}