using UnityEngine;

public class RoleEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    RoleInputComponent inputCom;
    public RoleInputComponent InputCom => inputCom;

    RoleFSMComponent fsmCom;
    public RoleFSMComponent FSMCom => fsmCom;

    float speed = 5f;

    GameObject rootGO;
    GameObject logicGO;
    GameObject rendererGO;

    public RoleEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Role);

        inputCom = new RoleInputComponent();
        fsmCom = new RoleFSMComponent();
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        logicGO = rootGO.transform.Find("LOGIC").gameObject;
        rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        Debug.Assert(logicGO != null, "logicGO != null");
        Debug.Assert(rendererGO != null, "rendererGO != null");
    }

    public void SetDontDestroyOnLoad() {
        GameObject.DontDestroyOnLoad(rootGO);
    }

    // Update logic immediately, and also update renderer's rotation immediately
    public void Move_Hor(int dir, float dt) {
        logicGO.transform.position += new Vector3(dir * speed * dt, 0, 0);
        logicGO.transform.rotation = Quaternion.Euler(0, dir == 1 ? 0 : 180, 0);
        rendererGO.transform.rotation = logicGO.transform.rotation;
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
