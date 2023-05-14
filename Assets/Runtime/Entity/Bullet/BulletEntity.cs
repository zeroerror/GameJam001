using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    BulletFSMComponent fsmCom;
    public BulletFSMComponent FSMCom => fsmCom;

    GameObject rootGO;

    GameObject logicGO;
    public Vector3 LogicPos => logicGO.transform.position;

    Rigidbody2D logicRB;
    public BoxCollider2D collider;
    GameObject rendererGO;

    // 子弹类型
    public BulletType bulletType;

    // 子弹伤害
    public int bulletDamage;

    // 子弹尺寸
    public Vector2 bulletSize;

    // 子弹吸血
    public int bloodThirst;

    // 散射
    public int fanOut;

    // 减速 percent
    public float slow;

    // 击退
    public float hitBackDis;

    // 飞行速度
    public float flySpeed;

    // PHX
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerEnter;
    public Action<EntityIDArgs, EntityIDArgs, Vector2, int, int> OnTriggerExit;

    public void Ctor() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Bullet);

        fsmCom = new BulletFSMComponent();
    }

    public void TearDown() {
        // PHX
        OnTriggerEnter = null;
        OnTriggerExit = null;
        GameObject.Destroy(rootGO);
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.logicRB = logicGO.GetComponent<Rigidbody2D>();
        this.collider = logicRB.GetComponent<BoxCollider2D>();
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;

        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(logicRB != null, "logicRB == null");
        Debug.Assert(collider != null, "collider == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
    }

    public void Fly(Vector2 dir) {
        logicRB.velocity = new Vector2(dir.x * flySpeed, dir.y * flySpeed);
    }

    /// <summary>
    /// 子弹反弹
    /// </summary>
    /// <param name="normal"></param>
    public void Bounce(Vector2 normal) {
        var velocity = logicRB.velocity;
        var dot = Vector2.Dot(velocity, normal);
        var bounce = velocity - 2 * dot * normal;
        logicRB.velocity = bounce;
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
        EntityIDComponent otherIDCom = null;
        var monster = other.GetComponentInParent<MonsterEntity>();
        if (monster != null) {
            otherIDCom = monster.IDCom;
        }

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

        var oneIDArgs = idCom.ToEntityIDArgs();
        var twoIDArgs = otherIDCom != null ? otherIDCom.ToEntityIDArgs() : new EntityIDArgs();
        var normal = other.transform.position - transform.position;
        normal = normal.x < 0 ? Vector2.left : Vector2.right;

        var layerMask_one = gameObject.layer;
        var layerMask_two = other.gameObject.layer;
        // OnTriggerExit?.Invoke(oneIDArgs, twoIDArgs, normal, layerMask_one, layerMask_two);
    }

}
