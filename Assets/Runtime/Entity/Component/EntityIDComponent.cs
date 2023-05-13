public struct EntityIDArgs {

    public EntityType entityType;
    public short entityID;
    public int typeID;
    public string entityName;
    public CampType campType;
    public ControlType controlType;

    public int fromFieldTypeID;

    public bool IsTheSameAs(in EntityIDArgs other) {
        return entityType == other.entityType
            && typeID == other.typeID
            && entityID == other.entityID;
    }

    public override string ToString() {
        return $"IDArgs: 实体类型 {entityType} 类型ID {typeID} 实体ID {entityID} 实体名称 {entityName} 阵营 {campType} 控制类型 {controlType} 来自关卡 {fromFieldTypeID}";
    }

}

public class EntityIDComponent {

    EntityType entityType;
    public EntityType EntityType => entityType;
    public void SetEntityType(EntityType v) => this.entityType = v;

    int typeID;
    public int TypeID => typeID;
    public void SetEntityType(int v) => this.typeID = v;

    int entityID;
    public int EntityID => entityID;
    public void SetEntityID(int v) => this.entityID = v;

    string entityName;
    public string EntityName => entityName;
    public void SetEntityName(string v) => this.entityName = v;

    CampType campType;
    public CampType AllyStatus => campType;
    public void SetAllyType(CampType value) => this.campType = value;

    ControlType controlType;
    public ControlType ControlType => controlType;
    public void SetControlType(ControlType value) => this.controlType = value;

    int fromFieldTypeID;
    public int FromFieldTypeID => fromFieldTypeID;
    public void SetFromFieldTypeID(int value) => this.fromFieldTypeID = value;

    EntityIDArgs father;
    public EntityIDArgs Father => father;

}
