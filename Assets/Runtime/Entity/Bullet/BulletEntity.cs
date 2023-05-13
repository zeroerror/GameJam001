using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    BulletFSMComponent fsmCom;
    public BulletFSMComponent FSMCom => fsmCom;

    GameObject rootGO;
    GameObject logicGO;
    Rigidbody2D logicRB;
    GameObject rendererGO;

    BulleAttrModel attrModel;
    public BulleAttrModel AttrModel => attrModel;
    public void SetBulletAttrModel(BulleAttrModel v) => this.attrModel = v;

    float flySpeed;
    public float FlySpeed => flySpeed;
    public void SetFlySpeed(float v) => this.flySpeed = v;

    // PHX
    public Action<EntityIDArgs, EntityIDArgs> OnTriggerEnter;
    public Action<EntityIDArgs, EntityIDArgs> OnTriggerExit;

    public void Ctor() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Bullet);

        fsmCom = new BulletFSMComponent();
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.logicRB = logicGO.GetComponent<Rigidbody2D>();
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;

        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
    }

    public void Fly(Vector2 dir) {
        logicRB.velocity = new Vector2(dir.x * flySpeed, dir.y * flySpeed);
    }

    // Easing renderer to logic
    public void EasingToDstPos(float dt) {
        rendererGO.transform.position = logicGO.transform.position;
    }

    public void SetPos(Vector2 pos) {
        var p = new Vector3(pos.x, pos.y, 0);
        logicGO.transform.position = p;
        rendererGO.transform.position = p;
    }

    public void SetRotation(Quaternion rotation) {
        logicGO.transform.rotation = rotation;
        rendererGO.transform.rotation = rotation;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D");
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponentInParent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

        if (otherIDCom != null) {
            OnTriggerEnter?.Invoke(idCom.ToEntityIDArgs(), otherIDCom.ToEntityIDArgs());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("OnTriggerExit2D");
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponentInParent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

        if (otherIDCom != null) {
            OnTriggerExit?.Invoke(idCom.ToEntityIDArgs(), otherIDCom.ToEntityIDArgs());
        }
    }

}
