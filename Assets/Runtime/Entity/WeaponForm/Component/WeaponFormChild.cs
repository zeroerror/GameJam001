using System;
using UnityEngine;

public class WeaponFormChild : MonoBehaviour {

    public WeaponFormEntity weaponFormEntity;

    public void Inject(WeaponFormEntity weaponFormEntity) {
        this.weaponFormEntity = weaponFormEntity;
    }

    // PHX
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerEnter;
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerExit;

    void OnTriggerEnter2D(Collider2D other) {
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponentInParent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
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
        var monster = other.GetComponentInParent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

        var idCom = weaponFormEntity.IDCom;
        var oneIDArgs = idCom.ToEntityIDArgs();
        var twoIDArgs = otherIDCom != null ? otherIDCom.ToEntityIDArgs() : new EntityIDArgs();
        var normal = other.transform.position - transform.position;
        normal = normal.x < 0 ? Vector2.left : Vector2.right;

        var layerMask_one = gameObject.layer;
        var layerMask_two = other.gameObject.layer;
    }


}