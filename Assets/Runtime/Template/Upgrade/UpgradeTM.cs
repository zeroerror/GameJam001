using UnityEngine;

[System.Serializable]
public struct UpgradeTM {

    [SerializeField] public UpgradeType upgradeType;
    [SerializeField] public string desc;
    [SerializeField] public Sprite icon;
    [SerializeField] public float factor;

    public override string ToString() {
        return $"upgradeType:{upgradeType}, desc:{desc},  factor:{factor}";
    }

}
