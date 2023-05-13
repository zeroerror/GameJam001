using System;
using System.Collections.Generic;
using UnityEngine;

public class PhxEventRepo {

    public Dictionary<ulong, PhxEventModel> enterDic;
    public Dictionary<ulong, PhxEventModel> exitDic;

    public PhxEventRepo() {
        enterDic = new Dictionary<ulong, PhxEventModel>();
        exitDic = new Dictionary<ulong, PhxEventModel>();
    }

    public void ClearAll() {
        enterDic.Clear();
        exitDic.Clear();
    }

    public void ForeachAllEnter(Action<PhxEventModel> action) {
        foreach (var pair in enterDic) {
            action(pair.Value);
        }
    }

    public void ForeachAllExit(Action<PhxEventModel> action) {
        foreach (var pair in exitDic) {
            action(pair.Value);
        }
    }

    public bool TryAdd(in EntityIDArgs one, in EntityIDArgs two) {
        var key = GetKey(one, two);
        var model = new PhxEventModel(one, two);
        if (enterDic.TryAdd(key, model)) {
            Debug.Log($"PHX - 添加碰撞事件\n{model}");
            return false;
        }

        Debug.LogWarning($"PHX - 添加碰撞事件 失败！！");
        return true;
    }

    /// <summary>
    /// 获取键值. 保证key1 > key2
    /// </summary>
    public ulong GetKey(in EntityIDArgs attacker, in EntityIDArgs victim) {
        var key1 = ComineToKey(attacker.entityType, attacker.entityID);
        var key2 = ComineToKey(victim.entityType, victim.entityID);
        if (key1 < key2) {
            key1 = key1 ^ key2;
            key2 = key1 ^ key2;
            key1 = key1 ^ key2;
        }
        return key1 << 32 | key2;
    }

    ulong ComineToKey(EntityType entityType, int entityID) => (ulong)entityType << 32 | (uint)entityID;

}