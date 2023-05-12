using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 伤害结算服务
/// </summary>
public class DamageArbitService {

    Dictionary<ulong, List<DamageRecordModel>> all;

    public DamageArbitService() {
        all = new Dictionary<ulong, List<DamageRecordModel>>();
    }

    /// <summary>
    /// 仲裁伤害
    /// </summary>
    public float ArbitrateDamage(int damage) {
        return damage;
    }

    /// <summary>
    /// 添加伤害记录
    /// </summary>s
    public void Add(float damage, in EntityIDArgs victim, in EntityIDArgs attacker) {
        var key = GetKey(attacker, victim);
        if (!all.TryGetValue(key, out var list)) {
            list = new List<DamageRecordModel>();
            all.Add(key, list);
        }

        var record = new DamageRecordModel();
        record.damage = damage;
        record.victim = victim;
        record.attacker = attacker;
        list.Add(record);
        Debug.Log($"伤害仲裁服务 - 添加伤害记录\n{record}");
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