using UnityEngine;

[System.Serializable]
public struct WeaponFormUpgradeTM {

    [SerializeField][Header("武器库 子弹容量")] public int ammoCapacity;
    [SerializeField][Header("武器库 射击间隔(S)")] public float shootCD;
    [SerializeField][Header("武器库 装填时间(S)")] public float reloadCD;
    [SerializeField][Header("子弹参数")] public BulletTM bulletTM;

}