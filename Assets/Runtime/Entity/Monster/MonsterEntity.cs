using UnityEngine;

public class MonsterEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    MonsterFSMComponent fsmCom;
    public MonsterFSMComponent FSMCom => fsmCom;

    int hp;
    public int HP => hp;
    public void SetHP(int v) => this.hp = v;

    FallPattern fallPattern;
    public FallPattern FallPattern => fallPattern;
    public void SetFallPattern(FallPattern v) => this.fallPattern = v;

    float fallSpeed;
    public float FallSpeed => fallSpeed;
    public void SetFallSpeed(float v) => this.fallSpeed = v;

    Vector2 size;
    public Vector2 Size => size;
    public void SetSize(Vector2 v) => this.size = v;

    GameObject rootGO;
    GameObject logicGO;
    Rigidbody2D logicRB;
    GameObject rendererGO;
    GameObject rendererBodyGO;
    GameObject rendererWeaponGO;

    public MonsterEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Monster);

        fsmCom = new MonsterFSMComponent();
    }

    public void Inject(GameObject rootGO, GameObject bodyMod) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.logicRB = logicGO.GetComponent<Rigidbody2D>();
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        this.rendererBodyGO = rendererGO.transform.Find("BODY").gameObject;
        this.rendererWeaponGO = rendererGO.transform.Find("WEAPON").gameObject;

        bodyMod.transform.SetParent(rendererBodyGO.transform, false);

        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicRB != null, "rootRB == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
        Debug.Assert(rendererWeaponGO != null, "rendererWeaponGO == null");
    }

    // Update logic rb immediately, and also update renderer's rotation immediately
    public void SetFallVelocity(float dt) {
        logicRB.velocity = new Vector2(0, fallSpeed);
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

    public Vector3 LogicPos => logicGO.transform.position;
    public Vector3 RendererPos => rendererGO.transform.position;

}
