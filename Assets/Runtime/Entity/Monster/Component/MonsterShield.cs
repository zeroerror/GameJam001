using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShield : MonoBehaviour {

    public MonsterEntity monsterEntity;

    List<BulletEntity> frame_block_bullets;
    public void ForeachFrameBlockBullets(Action<BulletEntity> action) {
        foreach (var bullet in frame_block_bullets) {
            action(bullet);
        }
    }
    public void ClearFrameBlockBulletList() => frame_block_bullets.Clear();

    public void Inject(MonsterEntity monsterEntity) {
        this.monsterEntity = monsterEntity;
        this.frame_block_bullets = new List<BulletEntity>();
    }

    // PHX
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerEnter;
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerExit;

    void OnTriggerEnter2D(Collider2D other) {
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        } else {
            var role = other.GetComponent<RoleEntity>();
            if (role != null) {
                otherIDCom = role.IDCom;
            } else {
                var bullet = other.GetComponent<BulletEntity>();
                if (bullet != null) {
                    otherIDCom = bullet.IDCom;
                    frame_block_bullets.Add(bullet);
                }
            }
        }

        var idCom = monsterEntity.IDCom;
        var oneIDArgs = idCom.ToEntityIDArgs();
        var twoIDArgs = otherIDCom != null ? otherIDCom.ToEntityIDArgs() : new EntityIDArgs();
        var normal = other.transform.position - transform.position;
        normal = normal.x < 0 ? Vector2.left : Vector2.right;

        var layerMask_one = gameObject.layer;
        var layerMask_two = other.gameObject.layer;
        OnTriggerEnter?.Invoke(oneIDArgs, twoIDArgs, normal, layerMask_one, layerMask_two);
    }

    void OnTriggerExit2D(Collider2D other) {
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

        var role = other.GetComponent<RoleEntity>();
        if (role != null) {
            otherIDCom = role.IDCom;
        }

        var idCom = monsterEntity.IDCom;
        var oneIDArgs = idCom.ToEntityIDArgs();
        var twoIDArgs = otherIDCom != null ? otherIDCom.ToEntityIDArgs() : new EntityIDArgs();
        var normal = other.transform.position - transform.position;
        normal = normal.x < 0 ? Vector2.left : Vector2.right;

        var layerMask_one = gameObject.layer;
        var layerMask_two = other.gameObject.layer;

        OnTriggerExit?.Invoke(oneIDArgs, twoIDArgs, normal, layerMask_one, layerMask_two);
    }


}