using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_UpgradeArgs {
    public int selectionID;
    public Sprite icon;
    public string desc;
}

public class Panel_Upgrade : MonoBehaviour {

    [SerializeField] Panel_Upgrade_Element selection1;
    [SerializeField] Panel_Upgrade_Element selection2;
    [SerializeField] Panel_Upgrade_Element selection3;
    Panel_Upgrade_Element[] all;

    Action<int> onSelectCallbackHandle;

    public void Ctor() {
        all = new Panel_Upgrade_Element[3];
        all[0] = selection1;
        all[1] = selection2;
        all[2] = selection3;
    }

    public void Open(Action<int> onSelectCallback, params Panel_UpgradeArgs[] args) {
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