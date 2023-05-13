using GameArki.FreeInput;

public class MainContext {

    public FreeInputCore freeInputCore;

    public RootTemplate rootTemplate;
    public RootRepo rootRepo;
    public RootService rootService;

    GameEntity gameEntity;
    public GameEntity GameEntity => gameEntity;

    public MainContext() {
        this.rootTemplate = new RootTemplate();
        this.rootRepo = new RootRepo();
        this.rootService = new RootService();

        this.gameEntity = new GameEntity();
    }

    public void Inject(FreeInputCore freeInputCore) {
        this.freeInputCore = freeInputCore;
    }

}