using UnityEngine;

public class WeaponFormEntity : MonoBehaviour {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    WeaponFormFSMComponent fsmCom;
    public WeaponFormFSMComponent FSMCom => fsmCom;

    GameObject rootGO;
    GameObject bodyGO;

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
    public RoleEntity roleEntity;  // 持有者

    public WeaponFormChild[] weaponFormChildren;
    public int weaponFormChildrenCount;

    public void Ctor() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.WeaponForm);

        fsmCom = new WeaponFormFSMComponent();

        attrModel = new WeaponFormAttrModel();

        this.curLevel = 1;
        this.weaponFormChildren = new WeaponFormChild[20];
    }

    public void Inject(GameObject rootGO) {
        this.rootGO = rootGO;
        this.bodyGO = rootGO.transform.Find("BODY").gameObject;
        Debug.Assert(rootGO != null, "rootGO == null");
        Debug.Assert(bodyGO != null, "bodyGO == null");

        var childCount = bodyGO.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            var child = bodyGO.transform.GetChild(i);
            if (child == null) {
                break;
            }
            var weaponFormChild = child.gameObject.AddComponent<WeaponFormChild>();
            weaponFormChild.Inject(this);
            weaponFormChildren[i] = weaponFormChild;
        }
        this.weaponFormChildrenCount = childCount;

        InitRenderer();
    }

    // 根据 WeaponFormUpgradeModel 升级
    public void LevelUp() {
        bodyGO.transform.GetChild(this.curLevel).gameObject.SetActive(true);
        this.curLevel++;
    }

    public void InitRenderer() {
        var childCount = bodyGO.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            var child = bodyGO.transform.GetChild(i);
            if (child == null) {
                break;
            }
            child.gameObject.SetActive(false);
        }
        bodyGO.transform.GetChild(this.curLevel - 1).gameObject.SetActive(true);
    }

    public void SetPos(Vector2 pos) {
        var p = new Vector3(pos.x, pos.y, 0);
        bodyGO.transform.position = p;
    }

}
