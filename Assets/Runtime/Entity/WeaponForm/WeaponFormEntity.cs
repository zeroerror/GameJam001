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
    WeaponFormAttrModel attrModel;
    public WeaponFormAttrModel AttrModel => attrModel;
    public void SetWeaponFormAttrModel(WeaponFormAttrModel v) => this.attrModel = v;

    public int curLevel;

    // Cache
    public int curBulletCount;     // 当前子弹数量

    public WeaponFormEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.WeaponForm);

        fsmCom = new WeaponFormFSMComponent();

        attrModel = new WeaponFormAttrModel();

        this.curLevel = 1;
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.rendererGO = rootGO.transform.Find("RENDERER").gameObject;
        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(rendererGO != null, "rendererGO == null");

        InitRenderer();
    }

    // 根据 WeaponFormUpgradeModel 升级
    public void LevelUp() {
        this.curLevel++;
        rendererGO.transform.GetChild(this.curLevel).gameObject.SetActive(true);
    }

    public void InitRenderer() {
        var childCount = rendererGO.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            var child = rendererGO.transform.GetChild(i);
            if (child == null) {
                break;
            }
            child.gameObject.SetActive(false);
        }
        rendererGO.transform.GetChild(this.curLevel).gameObject.SetActive(true);
    }

    public void SetPos(Vector2 pos) {
        var p = new Vector3(pos.x, pos.y, 0);
        rendererGO.transform.position = p;
    }

}
