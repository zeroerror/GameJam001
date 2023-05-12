using System;
using System.Collections.Generic;

public class RoleRepo {

    public Dictionary<int, RoleEntity> dic;

    public RoleRepo() {
        dic = new Dictionary<int, RoleEntity>();
    }

    public bool TryAdd(int typeID, RoleEntity role) {
        if (dic.ContainsKey(typeID)) {
            return false;
        }
        dic.Add(typeID, role);
        return true;
    }

    public bool TryGet(int typeID, out RoleEntity role) {
        return dic.TryGetValue(typeID, out role);
    }

    public bool TryRemove(int typeID) {
        return dic.Remove(typeID);
    }

    public void ForeachAll(Action<RoleEntity> action) {
        foreach (var pair in dic) {
            action(pair.Value);
        }
    }

}