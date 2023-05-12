public class IDService {

    ushort roleID;
    ushort bulletID;
    ushort monsterID;

    public IDService() {
        roleID = 0;
        bulletID = 0;
        monsterID = 0;
    }

    public ushort PickRoleID() {
        return roleID++;
    }

    public ushort PickBulletID() {
        return bulletID++;
    }

    public ushort PickMonsterID() {
        return monsterID++;
    }


}