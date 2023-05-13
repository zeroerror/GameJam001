using UnityEngine;

public class WeaponFormInfoModel {

    // 子弹容量
    int ammoCount;
    public int AmmoCount => ammoCount;

    // 子弹伤害
    float bulletDamage;
    public float BulletDamage => bulletDamage;
    public void SetBulletDamage(float value) => this.bulletDamage = value;

    // 子弹尺寸
    Vector2 bulletSize;
    public Vector2 BulletSize => bulletSize;
    public void SetBulletSize(Vector2 value) => this.bulletSize = value;

    // 射击间隔
    float shootCoolDown;
    public float ShootCoolDown => shootCoolDown;
    public void SetShootCoolDown(float value) => this.shootCoolDown = value;

    // 塔的装填时间
    float weaponFormAmmoRefill;
    public float WeaponFormAmmoRefill => weaponFormAmmoRefill;
    public void SetWeaponFormAmmoRefill(float value) => this.weaponFormAmmoRefill = value;

    BulleAttrModel bulleAttrModel;
    public BulleAttrModel BulleAttrModel => bulleAttrModel;

    public WeaponFormInfoModel() {
        this.bulleAttrModel = new BulleAttrModel();
    }

}