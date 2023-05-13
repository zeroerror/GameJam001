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

        // TM TODO

        var go = GameObject.Instantiate(prefab) as GameObject;
        role = new RoleEntity();
        role.Inject(go);

        return true;
    }

    public bool TryCreateBullet(int typeID, out BulletEntity bullet) {
        var prefab = Resources.Load("Bullet/go_template_bullet");
        if (prefab == null) {
            Debug.LogError("Bullet/go_template_bullet Not Found");
            bullet = null;
            return false;
        }

        // TM TODO

        var go = GameObject.Instantiate(prefab) as GameObject;
        bullet = new BulletEntity();
        bullet.Inject(go);

        return true;
    }

}