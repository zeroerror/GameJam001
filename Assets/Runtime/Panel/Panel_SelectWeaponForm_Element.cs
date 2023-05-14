using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SelectWeaponForm_Element : MonoBehaviour {

    [SerializeField] Image imgIcon;
    [SerializeField] Text txtDesc;
    [SerializeField] Button btn;
    int selectionID;

    public void Init(int selectionID, Sprite icon, string desc, Action<int> onClick) {
        this.selectionID = selectionID;
        imgIcon.sprite = icon;
        txtDesc.text = desc;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => {
            onClick.Invoke(selectionID);
            Debug.Log("Panel_SelectWeaponForm_Element:OnClick");
        });
    }

}