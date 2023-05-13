using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemplate {

    public Dictionary<int, BulletTM> dic;

    public BulletTemplate() {
        dic = new Dictionary<int, BulletTM>();
        var allSO = Resources.LoadAll<BulletSO>("Bullet");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
            Debug.Log($"子弹模板数据 +++ ====> {tm.typeID}");
        }
    }

    public bool TryGetNormalBullet(out BulletTM tm) {
        return TryGetByBulletType(BulletType.Normal, out tm);
    }

    public bool TryGetByBulletType(BulletType bulletType, out BulletTM tm) {
        foreach (var kvp in dic) {
            tm = kvp.Value;
            if (tm.bulletType == bulletType) {
                return true;
            }
        }

        tm = default(BulletTM);
        return false;
    }

}
