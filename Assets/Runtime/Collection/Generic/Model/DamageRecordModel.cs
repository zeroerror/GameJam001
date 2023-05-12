/// <summary>
/// 伤害记录数据
/// </summary>
public struct DamageRecordModel {

    public EntityIDArgs attacker;         // 攻击者
    public EntityIDArgs victim;           // 受害者
    public float damage;              // 伤害值


    public override string ToString() {
        return $"伤害记录数据\n攻击者:{attacker}\n受害者:{victim}\n伤害值:{damage}";
    }

}