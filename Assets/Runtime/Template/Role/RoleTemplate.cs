using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTemplate {

    public Dictionary<int, RoleTM> dic;

    public RoleTemplate() {
        dic = new Dictionary<int, RoleTM>();
        var allSO = Resources.LoadAll<RoleSO>("Role");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
            Debug.Log($"角色模板数据 +++ ====> {tm.typeID}");
        }
    }

    public bool TryGet(int typeID, out RoleTM tm) {
        return dic.TryGetValue(typeID, out tm);
    }

}
