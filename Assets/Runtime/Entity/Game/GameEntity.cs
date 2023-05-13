
public class GameEntity {

    GameFSMComponent fsmCom;
    public GameFSMComponent FSMCom => fsmCom;

    public GameEntity() {
        this.fsmCom = new GameFSMComponent();
    }

}
