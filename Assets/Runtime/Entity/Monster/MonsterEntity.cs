using System;
using UnityEngine;
using GameArki.FPEasing;

public class MonsterEntity : MonoBehaviour {

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

    public bool isDeadSpawnChildren;
    public int deadSpawnChildrenTypeID;

    GameObject rootGO;
    GameObject logicGO;
    Rigidbody2D logicRB;
    GameObject rendererGO;
    GameObject rendererBodyGO;
    GameObject rendererWeaponGO;
    SpriteRenderer mesh;

    Transform shieldRoot;

    // Phx
    public Action OnTriggerEnter;
    public Action OnTriggerExit;

    // temp
    public bool isNotValid;
    float time;

    public void Ctor() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Monster);

        fsmCom = new MonsterFSMComponent();
    }

    public void TearDown() {
        // PHX
        OnTriggerEnter = null;
        OnTriggerExit = null;
        isNotValid = true;
        GameObject.Destroy(rootGO);
    }

    public void Inject(GameObject rootGO, GameObject bodyMod) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.logicRB = logicGO.GetComponent<Rigidbody2D>();
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        this.rendererBodyGO = rendererGO.transform.Find("BODY").gameObject;
        this.rendererWeaponGO = rendererGO.transform.Find("WEAPON").gameObject;
        this.shieldRoot = rendererGO.transform.Find("ShieldRoot");

        mesh = bodyMod.GetComponentInChildren<SpriteRenderer>();
        bodyMod.transform.SetParent(rendererBodyGO.transform, false);

        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicRB != null, "rootRB == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
        Debug.Assert(rendererWeaponGO != null, "rendererWeaponGO == null");
        Debug.Assert(shieldRoot != null, "shieldRoot == null");
        Debug.Assert(mesh != null, "mesh == null");

    }

    public void Init() {
        if (fallPattern != FallPattern.RollingShieldFall) {
            shieldRoot.gameObject.SetActive(false);
        }
    }

    // Update logic rb immediately, and also update renderer's rotation immediately
    public void SetFallVelocity(float dt) {
        logicRB.velocity = new Vector2(0, fallSpeed);
    }

    public void Fall(float dt) {
        var velo = logicRB.velocity;
        if (fallPattern == FallPattern.SCurveFall) {
            float xOffset = 8;
            float xSpeed = 3f;
            velo.x = WaveHelper.SinWave(time, xOffset, xSpeed, 0);
            mesh.transform.Rotate(new Vector3(0, 0, 2));

        } else if (fallPattern == FallPattern.StraightFall) {
            velo.x = 0;
        }
        if (fallPattern == FallPattern.RollingShieldFall) {
            shieldRoot.Rotate(new Vector3(0, 0, 1));
        }
        velo.y = fallSpeed;
        time += dt;
        logicRB.velocity = velo;
    }

    // Easing renderer to logic
    public void EasingToDstPos(float dt) {
        if (rendererGO == null) return;
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

    void OnTriggerEnter2D(Collider2D other) {
        this.OnTriggerEnter?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other) {
        this.OnTriggerExit?.Invoke();
    }
}
