using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTemplate {

    public Dictionary<int, RoleTM> dic;

    public RoleTemplate() {
        dic = new Dictionary<int, RoleTM>();
        var allSO = Resources.LoadAll<RoleSO>("Runtime/Template/SO");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
        }
    }

}
