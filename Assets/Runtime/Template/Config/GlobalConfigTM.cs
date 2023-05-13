using UnityEngine;

[System.Serializable]
public struct GlobalConfigTM {

    [SerializeField] public float monsterSpawnPosY;
    [SerializeField] public Vector2 monsterSpawnPosXRange;
    [SerializeField] public Vector2 weaponFormPos1;
    [SerializeField] public Vector2 weaponFormPos2;
    [SerializeField] public Vector2 weaponFormPos3;
    [SerializeField] public int playerRoleTypeID;

    [Header("============================ 全局升级配置 ============================")]
    [SerializeField][Header("子弹吸血")] public int bloodThirst;
    [SerializeField][Header("子弹散射数量")] public int fanOut;
    [SerializeField][Header("子弹减速百分比")] public float slow;
    [SerializeField][Header("子弹击退距离(M)")] public float hitBackDis;
    [SerializeField][Header("武器库 子弹容量")] public int ammoCount;
    [SerializeField][Header("武器库 子弹伤害")] public float bulletDamage;
    [SerializeField][Header("武器库 子弹尺寸(M)")] public Vector2 bulletSize;
    [SerializeField][Header("武器库 射击间隔(S)")] public float shootCoolDown;
    [SerializeField][Header("武器库 装填时间(S)")] public float weaponFormAmmoRefill;

}
