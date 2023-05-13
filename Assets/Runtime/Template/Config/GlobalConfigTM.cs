using UnityEngine;

[System.Serializable]
public struct GlobalConfigTM {

    [SerializeField] public float monsterSpawnPosY;
    [SerializeField] public Vector2 monsterSpawnPosXRange;
    [SerializeField] public Vector2 weaponFormPos1;
    [SerializeField] public Vector2 weaponFormPos2;
    [SerializeField] public Vector2 weaponFormPos3;
    [SerializeField] public int playerRoleTypeID;

    [Header("============================ 升级配置 ============================")]
    [SerializeField] public WeaponFormUpgradeTM weaponFormTM;

    [Header("============================ 武器库初始配置 ============================")]
    [SerializeField] public WeaponFormUpgradeTM weaponFormInitTM;
    [Header("============================ 基地血量 ============================")]
    [SerializeField] public int baseHP;


}
