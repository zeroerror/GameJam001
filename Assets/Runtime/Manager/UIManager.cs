using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Panel_Login panel_Login;
    [SerializeField] Panel_Upgrade panel_upgrade;

    public void Ctor() {
        panel_Login.Hide();
        panel_upgrade.Ctor();
        panel_upgrade.Hide();
    }

    // - Login
    public void Login_Open(Action onPressAnyKey) {
        panel_Login.Init(onPressAnyKey);
        panel_Login.Show();
    }

    public void Login_Close() {
        panel_Login.Hide();
    }

    // - Upgrade
    public void Upgrade_Open(Action<int> onSelectCallback, params Panel_UpgradeArgs[] args) {
        Debug.Assert(args.Length == 3);
        panel_upgrade.Show();
        panel_upgrade.Open(onSelectCallback, args);
    }

    public void Tick(float dt) {
        if (panel_Login.isActiveAndEnabled) {
            panel_Login.Tick(dt);
        }
    }

}