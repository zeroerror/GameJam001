using System.Collections.Generic;

public class MonsterRepo {

    public Dictionary<int, MonsterEntity> dic;

    public MonsterRepo() {
        dic = new Dictionary<int, MonsterEntity>();
    }

    public bool TryAdd(int typeID, MonsterEntity monster) {
        if (dic.ContainsKey(typeID)) {
            return false;
        }
        dic.Add(typeID, monster);
        return true;
    }

    public bool TryGet(int typeID, out MonsterEntity monster) {
        return dic.TryGetValue(typeID, out monster);
    }

    public bool TryRemove(int typeID) {
        return dic.Remove(typeID);
    }

}