using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTemplate {

    public Dictionary<int, MonsterTM> dic;

    public MonsterTemplate() {
        dic = new Dictionary<int, MonsterTM>();
        var allSO = Resources.LoadAll<MonsterSO>("Runtime/Template/SO");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
        }
    }

}
