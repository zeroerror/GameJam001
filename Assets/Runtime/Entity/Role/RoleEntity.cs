using UnityEngine;

public class RoleEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    RoleInputComponent inputCom;
    public RoleInputComponent InputCom => inputCom;

    RoleFSMComponent fsmCom;
    public RoleFSMComponent FSMCom => fsmCom;

    float moveSpeed = 5f;
    float jumpSpeed = 10f;

    GameObject rootGO;
    GameObject logicGO;
    Rigidbody2D logicRB;
    GameObject rendererGO;

    public RoleEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Role);

        inputCom = new RoleInputComponent();
        fsmCom = new RoleFSMComponent();
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.logicRB = logicGO.GetComponent<Rigidbody2D>();
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;

        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicRB != null, "rootRB == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
    }

    public void SetDontDestroyOnLoad() {
        GameObject.DontDestroyOnLoad(rootGO);
    }

    // Update logic rb immediately, and also update renderer's rotation immediately
    public void Move_Hor(int dir, float dt) {
        logicRB.velocity = new Vector2(dir * moveSpeed, logicRB.velocity.y);

        var rot = Quaternion.Euler(0, dir == 1 ? 0 : 180, 0);
        logicGO.transform.rotation = rot;
        rendererGO.transform.rotation = rot;
    }

    public void StopMove_Hor(){
        logicRB.velocity = new Vector2(0, logicRB.velocity.y);
    }

    public void Jump() {
        logicRB.velocity = new Vector2(logicRB.velocity.x, jumpSpeed);
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

}
