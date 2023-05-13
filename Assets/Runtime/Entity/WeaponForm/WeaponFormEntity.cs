using UnityEngine;

public class WeaponFormEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    GameObject rootGO;
    GameObject logicGO;
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

        infoModel = new WeaponFormInfoModel();
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.logicGO = rootGO.transform.Find("LOGIC").gameObject;
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(logicGO != null, "logicGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");
    }

    // 根据 WeaponFormUpgradeModel 升级
    public void LevelUp() {
        
    }

}
