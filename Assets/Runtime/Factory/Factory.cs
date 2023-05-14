using UnityEngine;

public class Factory {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TryCreateRole(int typeID, out RoleEntity role) {
        var prefab = Resources.Load("Role/go_template_role");
        if (prefab == null) {
            Debug.LogError("Role/go_template_role Not Found");
            role = null;
            return false;
        }

        // TM 
        var roleTemplate = mainContext.rootTemplate.roleTemplate;
        if (!roleTemplate.TryGet(typeID, out var tm)) {
            Debug.LogError($"角色模板找不到 typeID:{typeID}");
            role = null;
            return false;
        }

        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        var logicGO = rootGO.transform.Find("LOGIC").gameObject;
        role = logicGO.AddComponent<RoleEntity>();
        role.Ctor();

        var bodyMod = tm.bodyMod;
        bodyMod = GameObject.Instantiate(bodyMod) as GameObject;

        role.Inject(rootGO, bodyMod);

        role.IDCom.SetTypeID(typeID);

        return true;
    }

    public bool TryCreateMonster(int typeID, out MonsterEntity monster) {
        var prefab = Resources.Load("Monster/go_template_monster");
        if (prefab == null) {
            Debug.LogError("Monster/go_template_monster Not Found");
            monster = null;
            return false;
        }

        // TM 
        var monsterTemplate = mainContext.rootTemplate.monsterTemplate;
        if (!monsterTemplate.TryGet(typeID, out var tm)) {
            Debug.LogError($"Template TryGet Failed!!! typeID:{typeID}");
            monster = null;
            return false;
        }

        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        var logicGO = rootGO.transform.Find("LOGIC").gameObject;
        monster = logicGO.AddComponent<MonsterEntity>();
        monster.Ctor();

        var bodyMod = tm.bodyMod;
        bodyMod = GameObject.Instantiate(bodyMod) as GameObject;
        monster.Inject(rootGO, bodyMod);

        monster.IDCom.SetTypeID(typeID);

        monster.SetHP(tm.hp);
        monster.SetFallPattern(tm.fallPattern);
        monster.SetFallSpeed(tm.fallSpeed);
        monster.SetSize(tm.size);

        monster.isDeadSpawnChildren = tm.isDeadSpawnChildren;
        monster.deadSpawnChildrenTypeID = tm.deadSpawnChildrenTypeID;

        monster.Init();

        return true;
    }

    public bool TryCreateBullet(BulletModel bulletModel,
                                out BulletEntity bullet) {
        var prefab = Resources.Load("Bullet/go_template_bullet");
        if (prefab == null) {
            Debug.LogError("Bullet/go_template_bullet Not Found");
            bullet = null;
            return false;
        }

        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        var logicGO = rootGO.transform.Find("LOGIC").gameObject;
        bullet = logicGO.AddComponent<BulletEntity>();
        bullet.Ctor();
        bullet.Inject(rootGO);

        // 子弹类型
        bullet.bulletType = bulletModel.bulletType;
        // 子弹伤害
        bullet.bulletDamage = bulletModel.bulletDamage;
        // 子弹尺寸
        bullet.SetBulletSize(bulletModel.bulletSize);
        // 子弹吸血
        bullet.bloodThirst = bulletModel.bloodThirst;
        // 散射
        bullet.fanOut = bulletModel.fanOut;
        // 减速 percent
        bullet.slow = bulletModel.slow;
        // 击退
        bullet.hitBackDis = bulletModel.hitBackDis;
        // 飞行速度
        bullet.flySpeed = bulletModel.flySpeed;

        // 子弹ICON
        bullet.SetBulletSprite(bulletModel.bulletIcon, bulletModel.themeColor);

        return true;
    }

}