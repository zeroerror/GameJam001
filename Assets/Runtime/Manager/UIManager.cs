using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Panel_Login panel_Login;

    public void Ctor() {
        panel_Login.enabled = false;
    }

    public void Login_Open(Action onPressAnyKey) {
        panel_Login.Init(onPressAnyKey);
        panel_Login.gameObject.SetActive(true);
    }

    public void Login_Close() {
        panel_Login.gameObject.SetActive(false);
    }

    public void Tick(float dt) {
        if (panel_Login.isActiveAndEnabled) {
            panel_Login.Tick(dt);
        }
    }

}