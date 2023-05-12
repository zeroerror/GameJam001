public class MainController {

    MainContext mainContext;
    RootDomain rootDomain;

    public MainController() {
        this.mainContext = new MainContext();
        this.rootDomain = new RootDomain(mainContext);
    }

    public void Tick() {

    }

}