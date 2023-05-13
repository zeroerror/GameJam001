using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemplate {

    Dictionary<int, BulletTM> dic;

    public BulletTemplate() {
        dic = new Dictionary<int, BulletTM>();
        var allSO = Resources.LoadAll<BulletSO>("Bullet");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add((int)tm.bulletType, tm);
            Debug.Log($"子弹 模板数据 +++ ====> {tm.bulletType}");
        }
    }

    public bool TryGet(BulletType bulletType, out BulletTM tm) {
        return dic.TryGetValue((int)bulletType, out tm);
    }

}
