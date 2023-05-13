using UnityEngine;

public struct BulleAttrModel {

    // 子弹伤害
    float bulletDamage;
    public float BulletDamage => bulletDamage;
    public void SetBulletDamage(float value) => this.bulletDamage = value;

    // 子弹尺寸
    Vector2 bulletSize;
    public Vector2 BulletSize => bulletSize;
    public void SetBulletSize(Vector2 value) => this.bulletSize = value;

    // 子弹吸血
    int bloodThirst;
    public int BloodThirst => bloodThirst;
    public void SetBloodThirst(int value) => this.bloodThirst = value;

    // 散射
    int fanOut;
    public int FanOut => fanOut;
    public void SetFanOut(int value) => this.fanOut = value;

    // 减速 percent
    float slow;
    public float Slow => slow;
    public void SetSlow(float value) => this.slow = value;

    // 击退
    float hitBackDis;
    public float HitBackDis => hitBackDis;
    public void SetHitBackDis(float v) => this.hitBackDis = v;
    float hitBackTime;
    public float HitBackTime => hitBackTime;
    public void SetHitBackTime(float v) => this.hitBackTime = v;

}