using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTemplate {

    Dictionary<int, MonsterTM> dic;

    public MonsterTemplate() {
        dic = new Dictionary<int, MonsterTM>();
        var allSO = Resources.LoadAll<MonsterSO>("Monster");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
            Debug.Log($"怪物模板数据 +++ ====> {tm.typeID}");
        }
    }

    public bool TryGet(int typeID, out MonsterTM tm) {
        return dic.TryGetValue(typeID, out tm);
    }

}
