using System;
using UnityEngine;

public class Panel_SelectWeaponFormArgs {
    public int selectionID;
    public Sprite icon;
    public string desc;
}

public class Panel_SelectWeaponForm : MonoBehaviour {

    [SerializeField] Panel_SelectWeaponForm_Element selection1;
    [SerializeField] Panel_SelectWeaponForm_Element selection2;
    [SerializeField] Panel_SelectWeaponForm_Element selection3;
    Panel_SelectWeaponForm_Element[] all;

    Action<int> onSelectCallbackHandle;

    public void Ctor() {
        all = new Panel_SelectWeaponForm_Element[3];
        all[0] = selection1;
        all[1] = selection2;
        all[2] = selection3;
    }

    public void Open(Action<int> onSelectCallback, params Panel_SelectWeaponFormArgs[] args) {
        this.onSelectCallbackHandle = onSelectCallback;
        for (int i = 0; i < args.Length; i += 1) {
            var arg = args[i];
            var ele = all[i];
            ele.Init(arg.selectionID, arg.icon, arg.desc, OnClick);
        }
    }

    void OnClick(int selectionID) {
        onSelectCallbackHandle.Invoke(selectionID);
        this.Hide();
    }

}