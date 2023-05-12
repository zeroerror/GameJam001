public class MainContext {

    public RootTemplate rootTemplate;
    public RootRepo rootRepo;

    public MainContext(){
        this.rootTemplate = new RootTemplate();
        this.rootRepo = new RootRepo();
    }

}