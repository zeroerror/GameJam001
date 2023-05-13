using System;
using System.Collections.Generic;

public class BulletRepo {

    public Dictionary<int, BulletEntity> dic;

    public BulletRepo() {
        dic = new Dictionary<int, BulletEntity>();
    }

    public bool TryAdd(BulletEntity bullet) {
        var entityID = bullet.IDCom.EntityID;
        if (dic.ContainsKey(entityID)) {
            return false;
        }
        dic.Add(entityID, bullet);
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