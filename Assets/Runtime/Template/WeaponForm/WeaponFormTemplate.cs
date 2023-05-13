using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFormTemplate {

    public Dictionary<int, WeaponFormTM> dic;

    public WeaponFormTemplate() {
        dic = new Dictionary<int, WeaponFormTM>();
        var allSO = Resources.LoadAll<WeaponFormSO>("Runtime/Template/SO");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
        }
    }

    public bool TryGet(BulletType bulletType, out WeaponFormTM tm) {
        return dic.TryGetValue((int)bulletType, out tm);
    }

}
