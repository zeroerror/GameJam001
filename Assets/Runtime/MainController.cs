using GameArki.FreeInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController {

    FreeInputCore freeInputCore;
    Factory factory;
    MainContext mainContext;
    RootDomain rootDomain;

    bool isReady;

    public MainController() {

        // Input Bind
        this.freeInputCore = new FreeInputCore();
        var inputSetter = freeInputCore.Setter;
        inputSetter.Bind(InputKeyCollection.MOVE_LEFT, KeyCode.A);
        inputSetter.Bind(InputKeyCollection.MOVE_RIGHT, KeyCode.D);
        inputSetter.Bind(InputKeyCollection.JUMP, KeyCode.Space);
        inputSetter.Bind(InputKeyCollection.SHOOT, KeyCode.Mouse0);

        this.factory = new Factory(mainContext);
        this.mainContext = new MainContext(freeInputCore);
        this.rootDomain = new RootDomain(mainContext, factory);

        // Load First Scene
        SceneManager.LoadScene("Game");
        isReady = true;
    }


    public void Update() {
        if (!isReady) {
            return;
        }

        // Input
        var roleDomain = rootDomain.roleDomain;
        roleDomain.BackInput();
    }

    public void Tick(float dt) {
        if (!isReady) {
            return;
        }

        var roleFSMDomain = rootDomain.roleFSMDomain;
        roleFSMDomain.TickFSM(dt);
    }

}