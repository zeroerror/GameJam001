using UnityEngine;

public class WeaponFormEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    WeaponFormFSMComponent fsmCom;
    public WeaponFormFSMComponent FSMCom => fsmCom;

    GameObject rootGO;
    GameObject rendererGO;

    BulletType bulletType;
    public BulletType BulletType => bulletType;
    public void SetBulletType(BulletType v) => this.bulletType = v;

    // Info
    WeaponFormInfoModel infoModel;
    public WeaponFormInfoModel InfoModel => infoModel;

    // Cache
    public int curBulletCount;     // 当前子弹数量

    public WeaponFormEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.WeaponForm);

        fsmCom = new WeaponFormFSMComponent();

        infoModel = new WeaponFormInfoModel();
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
    }

    // 根据 WeaponFormUpgradeModel 升级
    public void LevelUp() {

    }

    public void SetPos(Vector2 pos) {
        var p = new Vector3(pos.x, pos.y, 0);
        rendererGO.transform.position = p;
    }

}
