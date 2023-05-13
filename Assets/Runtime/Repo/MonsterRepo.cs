using System;
using System.Collections.Generic;

public class MonsterRepo {

    public Dictionary<int, MonsterEntity> dic;

    public MonsterRepo() {
        dic = new Dictionary<int, MonsterEntity>();
    }

    public bool TryAdd(MonsterEntity monster) {
        var entityID = monster.IDCom.EntityID;
        if (dic.ContainsKey(entityID)) {
            return false;
        }
        dic.Add(entityID, monster);
        return true;
    }

    public bool TryGet(int typeID, out MonsterEntity monster) {
        return dic.TryGetValue(typeID, out monster);
    }

    public bool TryRemove(int typeID) {
        return dic.Remove(typeID);
    }

    public void ForeachAll(Action<MonsterEntity> action) {
        foreach (var pair in dic) {
            action(pair.Value);
        }
    }

}