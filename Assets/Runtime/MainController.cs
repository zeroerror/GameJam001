using GameArki.FreeInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController {

    FreeInputCore freeInputCore;
    Factory factory;
    MainContext mainContext;
    RootDomain rootDomain;

    public MainController(VFXManager vfxManager, SFXManager sfxManager, UIManager uiManager, CameraManager cameraManager) {

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
        this.rootDomain.Inject(mainContext, factory);
        this.mainContext.Inject(vfxManager, sfxManager, uiManager, cameraManager, freeInputCore);
        
    }

    public void Enter() {
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