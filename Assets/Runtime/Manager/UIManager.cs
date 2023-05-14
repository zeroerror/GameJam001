using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] Panel_Login panel_Login;
    [SerializeField] Panel_SelectWeaponForm panel_selectWeaponForm;
    [SerializeField] Panel_ChooseType panel_chooseType;
    [SerializeField] Panel_Upgrade panel_upgrade;

    public void Ctor() {
        panel_Login.Hide();
        panel_selectWeaponForm.Hide();
        panel_chooseType.Hide();
        panel_upgrade.Hide();

        panel_selectWeaponForm.Ctor();
        panel_chooseType.Ctor();
        panel_upgrade.Ctor();
    }

    // - Login
    public void Login_Open(Action onPressAnyKey) {
        panel_Login.Init(onPressAnyKey);
        panel_Login.Show();
    }

    public void Login_Close() {
        panel_Login.Hide();
    }

    // - SelectWeaponForm
    public void SelectWeaponForm_Open(Action<int> onSelectCallback, params Panel_SelectWeaponFormArgs[] args) {
        Debug.Assert(args.Length == 3);
        panel_selectWeaponForm.Show();
        panel_selectWeaponForm.Open(onSelectCallback, args);
    }

    // - ChooseType
    public void ChooseType_Open(Action<int> onSelectCallback, params Panel_ChooseTypeArgs[] args) {
        Debug.Assert(args.Length == 3);
        panel_chooseType.Show();
        panel_chooseType.Open(onSelectCallback, args);
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