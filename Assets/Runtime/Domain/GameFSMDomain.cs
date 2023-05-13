using UnityEngine;

public class GameFSMDomain {

    MainContext mainContext;
    RoleDomain roleDomain;

    public void Inject(MainContext mainContext,RoleDomain roleDomain) {
        this.mainContext = mainContext;
        this.roleDomain = roleDomain;
    }

    public void TickFSM(float dt) {
        var gameEntity = mainContext.GameEntity;
        var fsmCom = gameEntity.FSMCom;
        var state = fsmCom.State;
        if (state == GameFSMState.None) {
            return;
        }

        if (state == GameFSMState.Lobby) {
            TickLobby(gameEntity, dt);
        } else if (state == GameFSMState.Battle) {
            TickBattle(gameEntity, dt);
        }

        TickAny(gameEntity, dt);
    }

    public void TickAny(GameEntity gameEntity, float dt) {
    }

    public void TickLobby(GameEntity gameEntity, float dt) {

    }

    public void TickBattle(GameEntity gameEntity, float dt) {
        var stateModel = gameEntity.FSMCom.BattleStateModel;
        if (stateModel.IsEntering) {
            stateModel.SetIsEntering(false);
            roleDomain.TrySpawnPlayerRole();
        }

        // ========= Input
        roleDomain.BackInput();

        // ========= Renderer
        roleDomain.EasingRenderer(dt);
    }

    public void Enter_Lobby(GameEntity Game) {
        var fsmCom = Game.FSMCom;
        fsmCom.EnterLobby();
    }

    public void Enter_Battle(GameEntity Game) {
        var fsmCom = Game.FSMCom;
        fsmCom.EnterBattle();
    }

}