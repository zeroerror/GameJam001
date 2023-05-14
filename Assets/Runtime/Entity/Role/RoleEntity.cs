using System;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    RoleInputComponent inputCom;
    public RoleInputComponent InputCom => inputCom;

    RoleFSMComponent fsmCom;
    public RoleFSMComponent FSMCom => fsmCom;

    WeaponFormSlotComponent weaponFormSlotCom;
    public WeaponFormSlotComponent WeaponFormSlotCom => weaponFormSlotCom;

    float moveSpeed = 5f;
    float jumpSpeed = 10f;

    GameObject rootGO;
    GameObject logicGO;
    Rigidbody2D logicRB;
    GameObject rendererGO;
    GameObject rendererBodyGO;
    GameObject rendererWeaponGO;

    public void Ctor() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Role);

        inputCom = new RoleInputComponent();
        fsmCom = new RoleFSMComponent();
        weaponFormSlotCom = new WeaponFormSlotComponent();
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

    public void SetDontDestroyOnLoad() {
        GameObject.DontDestroyOnLoad(rootGO);
    }

    // Update logic rb immediately, and also update renderer's rotation immediately
    public void SetMoveVelocity(int dir, float dt) {
        logicRB.velocity = new Vector2(dir * moveSpeed, logicRB.velocity.y);

        var rot = Quaternion.Euler(0, dir == 1 ? 0 : 180, 0);
        logicGO.transform.rotation = rot;

        var stayWeaponRot = rendererWeaponGO.transform.rotation;
        rendererGO.transform.rotation = rot;
        rendererWeaponGO.transform.rotation = stayWeaponRot;
    }

    public void StopMove_Hor() {
        logicRB.velocity = new Vector2(0, logicRB.velocity.y);
    }

    public void Jump() {
        logicRB.velocity = new Vector2(logicRB.velocity.x, jumpSpeed);
    }

    public void AnimWeaponToPos(Vector3 pos) {
        var weaponTF = rendererWeaponGO.transform;
        var dir = (pos - weaponTF.position).normalized;
        weaponTF.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector3.forward);
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

    public void SetWeaponSprite(Sprite sprite) {
        var spriteRenderer = rendererWeaponGO.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

}
