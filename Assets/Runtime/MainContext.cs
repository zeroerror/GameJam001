using GameArki.FreeInput;

public class MainContext {

    public RootTemplate rootTemplate;
    public RootRepo rootRepo;
    public RootService rootService;
    public FreeInputCore freeInputCore;

    public MainContext(FreeInputCore freeInputCore) {
        this.freeInputCore = freeInputCore;

        this.rootTemplate = new RootTemplate();
        this.rootRepo = new RootRepo();
    }

}