using System;
using System.Collections.Generic;

public class RoleRepo {

    public Dictionary<int, RoleEntity> dic;

    RoleEntity playerRole;
    public RoleEntity PlayerRole => playerRole;
    public void SetPlayerRole(RoleEntity role) => playerRole = role;

    public RoleRepo() {
        dic = new Dictionary<int, RoleEntity>();
    }

    public bool TryAdd(RoleEntity role) {
        var entityID = role.IDCom.EntityID;
        if (dic.ContainsKey(entityID)) {
            return false;
        }
        dic.Add(entityID, role);
        return true;
    }

    public bool TryGet(int entityID, out RoleEntity role) {
        return dic.TryGetValue(entityID, out role);
    }

    public bool TryRemove(int entityID) {
        return dic.Remove(entityID);
    }

    public void ForeachAll(Action<RoleEntity> action) {
        foreach (var pair in dic) {
            action(pair.Value);
        }
    }

}