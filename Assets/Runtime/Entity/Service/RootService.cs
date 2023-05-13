public class RootService {

    public IDService idService;
    public DamageArbitService damageArbitService;

    public RootService() {
        this.idService = new IDService();
        this.damageArbitService = new DamageArbitService();
    }

}