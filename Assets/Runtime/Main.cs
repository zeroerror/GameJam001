using UnityEngine;
using GameArki.FreeInput;

public class Main : MonoBehaviour {

    MainController mainController;

    // First Party
    FreeInputCore freeInputCore;

    void Awake() {

        GameObject.DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 120;
        Physics2D.simulationMode = SimulationMode2D.Script;

        // Controller
        this.mainController = new MainController();

    }

    void Update() {
        this.mainController.Update();
    }

    void FixedUpdate() {
        var dt = Time.fixedDeltaTime;
        this.mainController.Tick(dt);
    }

}
