using System;
using System.Collections.Generic;

public class BulletRepo {

    public Dictionary<int, BulletEntity> dic;

    public BulletRepo() {
        dic = new Dictionary<int, BulletEntity>();
    }

    public bool TryAdd(int typeID, BulletEntity bullet) {
        if (dic.ContainsKey(typeID)) {
            return false;
        }
        dic.Add(typeID, bullet);
        return true;
    }

    public bool TryGet(int typeID, out BulletEntity bullet) {
        return dic.TryGetValue(typeID, out bullet);
    }

    public bool TryRemove(int typeID) {
        return dic.Remove(typeID);
    }

    public void ForeachAll(Action<BulletEntity> action) {
        foreach (var pair in dic) {
            action(pair.Value);
        }
    }

}