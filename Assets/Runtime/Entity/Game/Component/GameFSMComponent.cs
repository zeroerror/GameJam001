public class GameFSMComponent {

    GameFSMState state;
    public GameFSMState State => state;

    GameFSMStateModel_Lobby lobbyStateModel;
    public GameFSMStateModel_Lobby LobbyStateModel => lobbyStateModel;

    GameFSMStateModel_Battle battleStateModel;
    public GameFSMStateModel_Battle BattleStateModel => battleStateModel;

    public GameFSMComponent() {
        state = GameFSMState.None;
        lobbyStateModel = new GameFSMStateModel_Lobby();
        battleStateModel = new GameFSMStateModel_Battle();
    }

    public void Reset() {
        state = GameFSMState.None;
        lobbyStateModel.Reset();
        battleStateModel.Reset();
    }

    public void EnterLobby() {
        var stateModel = lobbyStateModel;
        stateModel.Reset();
        stateModel.SetIsEntering(true);

        state = GameFSMState.Lobby;
    }

    public void EnterBattle() {
        var stateModel = battleStateModel;
        stateModel.Reset();
        stateModel.SetIsEntering(true);
        
        state = GameFSMState.Battle;
    }

}