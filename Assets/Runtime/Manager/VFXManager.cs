using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour {

    [SerializeField] GameObject vfx_hit_bullet;
    [SerializeField] GameObject vfx_hit_homebase;
    [SerializeField] GameObject vfx_shoot_normal;
    [SerializeField] GameObject vfx_shoot_rocket;

    // 普通子弹-击中目标
    public void Hit_Bullet(Vector3 pos, Color color) {
        SpawnVFXWithColor(vfx_hit_bullet, pos, color);
        Debug.Log(color);
    }

    // 基地-被击中
    public void Hit_Homebase(Vector3 pos) {
        SpawnVFX(vfx_hit_homebase, pos);
    }

    // 发射-普通子弹
    public void Shoot_Normal(Vector3 pos, Color color) {
        SpawnVFXWithColor(vfx_shoot_normal, pos, color);
    }

    // 发射-火箭
    public void Shoot_Rocket(Vector3 pos) {
        SpawnVFX(vfx_shoot_rocket, pos);
    }

    void SpawnVFX(GameObject prefab, Vector3 pos) {
        var go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        GameObject.Destroy(go, 1f);
    }
    void SpawnVFXWithColor(GameObject prefab, Vector3 pos, Color color) {
        var go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        go.GetComponent<ParticleSystemRenderer>().material.SetColor("_GlowColor", color);
        GameObject.Destroy(go, 1f);
    }

}