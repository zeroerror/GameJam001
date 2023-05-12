public class MonsterDomain {

    MainContext mainContext;

    public MonsterDomain(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public bool TryCreateMonster() {
        return false;
    }

}