using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemplate {

    Dictionary<int, BulletTM> dic;

    public BulletTM[] tmArray;

    public BulletTemplate() {
        var allSO = Resources.LoadAll<BulletSO>("Bullet");
        var len = allSO.Length;
        dic = new Dictionary<int, BulletTM>(len);
        tmArray = new BulletTM[len];
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add((int)tm.bulletType, tm);
            tmArray[i] = tm;
            Debug.Log($"子弹 模板数据 +++ ====> {tm.bulletType}");
        }

    }

    public bool TryGet(BulletType bulletType, out BulletTM tm) {
        return dic.TryGetValue((int)bulletType, out tm);
    }

}
