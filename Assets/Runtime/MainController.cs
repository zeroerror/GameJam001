using GameArki.FreeInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController {

    FreeInputCore freeInputCore;
    Factory factory;
    MainContext mainContext;
    RootDomain rootDomain;

    public MainController(VFXManager vfxManager) {

        // Input Bind
        this.freeInputCore = new FreeInputCore();
        var inputSetter = freeInputCore.Setter;
        inputSetter.Bind(InputKeyCollection.MOVE_LEFT, KeyCode.A);
        inputSetter.Bind(InputKeyCollection.MOVE_RIGHT, KeyCode.D);
        inputSetter.Bind(InputKeyCollection.JUMP, KeyCode.Space);
        inputSetter.Bind(InputKeyCollection.SHOOT, KeyCode.Mouse0);

        this.factory = new Factory();
        this.mainContext = new MainContext();
        this.rootDomain = new RootDomain();

        this.factory.Inject(mainContext);
        this.mainContext.Inject(vfxManager, freeInputCore);
        this.rootDomain.Inject(mainContext, factory);

        // Load First Scene
        SceneManager.LoadScene("Game");
        var gameFSMDomain = rootDomain.gameFSMDomain;
        gameFSMDomain.Enter_Battle(mainContext.GameEntity);
    }

    public void Update(float dt) {
        var gameFSMDomain = rootDomain.gameFSMDomain;
        gameFSMDomain.TickFSM(dt);
    }

}