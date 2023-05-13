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

        role = new RoleEntity();

        role.IDCom.SetTypeID(typeID);

        var bodyMod = tm.bodyMod;
        bodyMod = GameObject.Instantiate(bodyMod) as GameObject;
        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        role.Inject(rootGO, bodyMod);

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

        monster = new MonsterEntity();

        monster.IDCom.SetTypeID(typeID);

        monster.SetHP(tm.hp);
        monster.SetFallPattern(tm.fallPattern);
        monster.SetFallSpeed(tm.fallSpeed);
        monster.SetSize(tm.size);

        var bodyMod = tm.bodyMod;
        bodyMod = GameObject.Instantiate(bodyMod) as GameObject;
        var rootGO = GameObject.Instantiate(prefab) as GameObject;
        monster.Inject(rootGO, bodyMod);

        return true;
    }

    public bool TryCreateBullet(BulletType bulletType, in BulleAttrModel bulleAttrModel, out BulletEntity bullet) {
        var prefab = Resources.Load("Bullet/go_template_bullet");
        if (prefab == null) {
            Debug.LogError("Bullet/go_template_bullet Not Found");
            bullet = null;
            return false;
        }

        var bulletTemplate = mainContext.rootTemplate.bulletTemplate;
        if (!bulletTemplate.TryGetNormalBullet(out var tm)) {
            Debug.LogError($"Template TryGetNormalBullet Failed!!!");
            bullet = null;
            return false;
        }

        bullet = new BulletEntity();
        bullet.IDCom.SetTypeID(tm.typeID);

        var go = GameObject.Instantiate(prefab) as GameObject;
        bullet.Inject(go);

        // Set
        bullet.SetBulletAttrModel(bulleAttrModel);

        return true;
    }

}