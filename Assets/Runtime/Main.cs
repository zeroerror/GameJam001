using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] VFXManager vfxManager;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] CameraManager cameraManager;

    MainController mainController;

    void Start() {

        GameObject.DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 120;
        Physics2D.simulationMode = SimulationMode2D.Script;

        // Controller
        this.mainController = new MainController(vfxManager, sfxManager, uiManager, cameraManager);

        cameraManager.Ctor();

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

        #region CameraDebug
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                cameraManager.Shake_Normal();
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                cameraManager.Shake_Rocket_Shoot();
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                cameraManager.Shake_Rocket_Hit();
            }
        }
        #endregion

        cameraManager.Tick(dt);

    }

}
