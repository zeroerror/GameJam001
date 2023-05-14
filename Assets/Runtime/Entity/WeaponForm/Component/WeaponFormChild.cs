using System;
using UnityEngine;

public class WeaponFormChild : MonoBehaviour {

    public WeaponFormEntity weaponFormEntity;

    GameObject bar;
    float originWidth;

    public void Inject(WeaponFormEntity weaponFormEntity, BulletTM tm) {
        this.weaponFormEntity = weaponFormEntity;
        if (name == "1") {
            bar = transform.GetChild(0).GetChild(0).gameObject;
            originWidth = bar.transform.localScale.x;
            var color = bar.GetComponent<SpriteRenderer>().color;
            color = new Color(tm.themeColor.r, tm.themeColor.g, tm.themeColor.b, color.a);
            bar.GetComponent<SpriteRenderer>().color = color;
            Debug.Log("设置弹药数量AA");
        }
    }

    public void SetBulletCount(int count, int maxCount) {
        var scaleX = (float)count / maxCount;
        bar.transform.localScale = new Vector3(originWidth * scaleX, bar.transform.lossyScale.y, 1);
        Debug.Log("设置弹药数量:" + count + "; " + maxCount);

    }

    // PHX
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerEnter;
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerExit;

    void OnTriggerEnter2D(Collider2D other) {
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

        var role = other.GetComponent<RoleEntity>();
        if (role != null) {
            otherIDCom = role.IDCom;
        }

        var idCom = weaponFormEntity.IDCom;
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

        var idCom = weaponFormEntity.IDCom;
        var oneIDArgs = idCom.ToEntityIDArgs();
        var twoIDArgs = otherIDCom != null ? otherIDCom.ToEntityIDArgs() : new EntityIDArgs();
        var normal = other.transform.position - transform.position;
        normal = normal.x < 0 ? Vector2.left : Vector2.right;

        var layerMask_one = gameObject.layer;
        var layerMask_two = other.gameObject.layer;

        OnTriggerExit?.Invoke(oneIDArgs, twoIDArgs, normal, layerMask_one, layerMask_two);
    }


}