using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTemplate {

    Dictionary<int, WaveTM> dic;

    public WaveTemplate() {
        dic = new Dictionary<int, WaveTM>();
        var allSO = Resources.LoadAll<WaveSO>("Wave");
        for (int i = 0; i < allSO.Length; i++) {
            var tm = allSO[i].tm;
            dic.Add(tm.typeID, tm);
            Debug.Log($"战斗波次 模板数据 +++ ====> {tm.typeID}");
        }
    }

    public bool TryGetWaveTM(out WaveTM tm) {
        tm = default;

        foreach (var kvp in dic) {
            tm = kvp.Value;
            return true;
        }

        Debug.LogError("没有找到战斗波次模板数据");
        return false;
    }

    public bool TryGet(int typeID, out WaveTM tm) {
        return dic.TryGetValue(typeID, out tm);
    }

}
