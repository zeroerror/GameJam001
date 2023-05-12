using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemplate {

    public Dictionary<int, BulletTM> dic;

    public BulletTemplate() {
        dic = new Dictionary<int, BulletTM>();
        var allSO = Resources.LoadAll<BulletSO>("Runtime/Template/SO");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
        }
    }

}
