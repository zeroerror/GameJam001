public enum CampType {

    None,
    One,    // 玩家阵营
    Two,    // 敌人阵营
    Neutral,

}

public static class AllyTypeExtension {

    public static bool IsAlly(this CampType self, CampType other) {
        return self == other;
    }

    public static bool IsEnemy(this CampType self, CampType other) {
        if (self == CampType.Neutral || other == CampType.Neutral) {
            return false;
        }

        if (self == CampType.None || other == CampType.None) {
            return false;
        }

        return self != other;
    }

}