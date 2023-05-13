using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] VFXManager vfxManager;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] UIManager uiManager;

    MainController mainController;

    void Start() {

        GameObject.DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 120;
        Physics2D.simulationMode = SimulationMode2D.Script;

        // Controller
        this.mainController = new MainController(vfxManager, sfxManager, uiManager);

        uiManager.Ctor();

        uiManager.Login_Open(() => {
            mainController.Enter();
            uiManager.Login_Close();
        });

    }

    void Update() {
        var dt = Time.deltaTime;
        uiManager.Tick(dt);
        mainController.Update(dt);
    }

}
