using UnityEngine;
using GameArki.FreeInput;

public class Main : MonoBehaviour {

    [SerializeField] VFXManager vfxManager;
    MainController mainController;

    // First Party
    FreeInputCore freeInputCore;

    void Awake() {

        GameObject.DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 120;
        Physics2D.simulationMode = SimulationMode2D.Script;

        // Controller
        this.mainController = new MainController(vfxManager);

    }

    void Update() {
        var dt = Time.deltaTime;
        this.mainController.Update(dt);
    }

}
