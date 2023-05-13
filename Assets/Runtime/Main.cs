using UnityEngine;
using GameArki.FreeInput;

public class Main : MonoBehaviour {

    [SerializeField] VFXManager vfxManager;
    [SerializeField] SFXManager sfxManager;
    MainController mainController;

    // First Party
    FreeInputCore freeInputCore;

    void Start() {

        GameObject.DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 120;
        Physics2D.simulationMode = SimulationMode2D.Script;

        // Controller
        this.mainController = new MainController(vfxManager, sfxManager);

    }

    void Update() {
        var dt = Time.deltaTime;
        this.mainController.Update(dt);
    }

}
