using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    [SerializeField] AudioSource audioSourcePrefab;
    [SerializeField] AudioSource shootPlayer;
    [SerializeField] AudioSource bgmPlayer;
    [SerializeField] AudioSource uiPlayer;

    [SerializeField] AudioClip bgm_login;
    [SerializeField] AudioClip bgm_combat;

    [SerializeField] AudioClip gun_normal;
    [SerializeField] AudioClip gun_normal_hit;
    [SerializeField] AudioClip gun_laser;
    [SerializeField] AudioClip gun_laser_hit;
    [SerializeField] AudioClip gun_triplet;
    [SerializeField] AudioClip gun_triplet_hit;
    [SerializeField] AudioClip gun_rocket;
    [SerializeField] AudioClip gun_rocket_hit;
    [SerializeField] AudioClip gun_bubble;
    [SerializeField] AudioClip gun_bubble_hit;
    [SerializeField] AudioClip gun_bubble_impluse;

    [SerializeField] AudioClip behit_homebase;

    [SerializeField] AudioClip ui_click;
    [SerializeField] AudioClip ui_upgrade;

    Dictionary<BulletType, AudioClip> allShoot;
    Dictionary<BulletType, AudioClip> allHit;

    public void Ctor() {
        allShoot = new Dictionary<BulletType, AudioClip>();
        allShoot.Add(BulletType.Normal, gun_normal);
        allShoot.Add(BulletType.Laser, gun_laser);
        allShoot.Add(BulletType.Triplet, gun_triplet);
        allShoot.Add(BulletType.Rocket, gun_rocket);
        allShoot.Add(BulletType.Bubble, gun_bubble);

        allHit = new Dictionary<BulletType, AudioClip>();
        allHit.Add(BulletType.Normal, gun_normal_hit);
        allHit.Add(BulletType.Laser, gun_laser_hit);
        allHit.Add(BulletType.Triplet, gun_triplet_hit);
        allHit.Add(BulletType.Rocket, gun_rocket_hit);
        allHit.Add(BulletType.Bubble, gun_bubble_hit);
    }

    public void BGM_Login() {
        bgmPlayer.clip = bgm_login;
        bgmPlayer.Play();
    }

    public void BGM_Combat() {
        bgmPlayer.clip = bgm_combat;
        bgmPlayer.Play();
    }

    public void SFX_Gun_Shoot(BulletType bulletType) {
        bool has = allShoot.TryGetValue(bulletType, out var clip);
        if (has) {
            shootPlayer.clip = clip;
            shootPlayer.Play();
        }
    }

    public void SFX_Bullet_Hit(BulletType bulletType) {
        bool has = allHit.TryGetValue(bulletType, out var clip);
        if (has) {
            PlayOnce(clip);
        }
    }

    public void SFX_Bubble_Impluse() {
        PlayOnce(gun_bubble_impluse);
    }

    public void SFX_Homebase_Behit() {
        PlayOnce(behit_homebase);
    }

    public void UI_Click() {
        uiPlayer.PlayOneShot(ui_click);
    }

    public void UI_Upgrade() {
        uiPlayer.PlayOneShot(ui_upgrade);
    }

    void PlayOnce(AudioClip clip) {
        var player = GameObject.Instantiate(audioSourcePrefab);
        player.clip = clip;
        player.Play();
        GameObject.Destroy(player.gameObject, clip.length);
    }

}