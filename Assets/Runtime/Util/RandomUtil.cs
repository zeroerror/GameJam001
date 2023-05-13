using UnityEngine;
using System.Collections.Generic;

public static class RandomUtil {

    // 以顶部的中心为(0, 0), 生成的结果是基于它的偏移量
    //     ↓
    // ----------
    // |        |
    // |        |
    // ----------
    public static List<Vector2> GenRandomPosOffsetArray(int genNum, int xGridCount, int yGridOffset) {
        var result = new List<Vector2>();
        while (result.Count <= genNum) {
            var generatedPos = new Vector2Int(Random.Range(0, xGridCount), Random.Range(0, yGridOffset));
            if (result.Contains(generatedPos)) {
                continue;
            }
            result.Add(generatedPos);
        }
        return result;
    }

}