using UnityEngine;

[System.Serializable]
public struct GlobalConfigTM {

    [SerializeField] public float monsterSpawnPosY;
    [SerializeField] public Vector2 monsterSpawnPosXRange;
    [SerializeField] public Vector2 weaponFormPos1;
    [SerializeField] public Vector2 weaponFormPos2;
    [SerializeField] public Vector2 weaponFormPos3;
    [SerializeField] public int playerRoleTypeID;

    [SerializeField][Header("武器库闲置多久后开始装填")] public float weaponFormIdleToReloadCD;
    [SerializeField][Header("子弹最长飞行时间")] public float bulletMaxFlyTime;

    [Header("============================ 武器库初始配置 ============================")]
    [SerializeField][Header("武器库 子弹容量")] public int bulletCapacity_init;
    [SerializeField][Header("武器库 射击间隔(S)")] public float shootCD_init;
    [SerializeField][Header("武器库 装填时间(S)")] public float reloadCD_init;
    [SerializeField][Header("初始子弹类型")] public BulletType bulletType_init;
    [Header("============================ 基地血量 ============================")]
    [SerializeField] public int baseHP;

}
