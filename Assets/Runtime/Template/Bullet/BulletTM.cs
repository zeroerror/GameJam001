using UnityEngine;

[System.Serializable]
public struct BulletTM {

    [SerializeField][Header("子弹类型")] public BulletType bulletType;
    [SerializeField][Header("子弹吸血")] public int bloodThirst;
    [SerializeField][Header("子弹散射数量")] public int fanOut;
    [SerializeField][Header("子弹减速百分比")] public float slow;
    [SerializeField][Header("子弹击退距离(M)")] public float hitBackDis;
    [SerializeField][Header("子弹伤害")] public int bulletDamage;
    [SerializeField][Header("子弹尺寸(M)")] public Vector2 bulletSize;
    [SerializeField][Header("子弹飞行速度(M/S)")] public float flySpeed;

    [SerializeField][Header("子弹ICON")] public Sprite bulletIcon;
    [SerializeField][Header("武器ICON")] public Sprite weaponIcon;

}
