using UnityEngine;

public class WeaponFormAttrModel {

    // 子弹容量
    public int ammoCount;
    // 子弹伤害
    public int bulletDamage;
    // 子弹尺寸
    public Vector2 bulletSize;
    // 射击间隔
    public float shootCoolDown;
    // 塔的装填时间
    public float bulletReloadCD;
    // 子弹吸血
    public int bloodThirst;
    // 散射
    public int fanOut;
    // 减速 percent
    public float slow;
    // 击退
    public float hitBackDis;

    public int hp;
    public int baseHP;

    public WeaponFormAttrModel() {
    }

}