using UnityEngine;

public class Factory {

    MainContext mainContext;

    public Factory(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TryCreateRole(int typeID, out RoleEntity role) {
        var prefab = Resources.Load("Role/go_role_template");
        if (prefab == null) {
            Debug.LogError("Role/go_role_template Not Found");
            role = null;
            return false;
        }

        var go = GameObject.Instantiate(prefab) as GameObject;
        role = new RoleEntity();
        role.Inject(go);

        return true;
    }


}