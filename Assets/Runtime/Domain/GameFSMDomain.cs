using UnityEngine;

public class GameFSMDomain {

    MainContext mainContext;
    RootDomain rootDomain;
    public void Inject(MainContext mainContext, RootDomain rootDomain) {
        this.mainContext = mainContext;
        this.rootDomain = rootDomain;
    }

    float resTime;
    void TickResTime(float dt) => resTime += dt;

    int PickRealTickCount() {
        var intervalTime = 1f / 120f;
        int tickCount = 0;
        while (resTime >= intervalTime) {
            resTime -= intervalTime;
            tickCount++;
        }

        return tickCount;
    }

    public void TickFSM(float dt) {
        var gameEntity = mainContext.GameEntity;
        var fsmCom = gameEntity.FSMCom;
        var state = fsmCom.State;
        if (state == GameFSMState.None) {
            return;
        }

    // ========= Input
        var roleDomain = rootDomain.roleDomain;
        roleDomain.PlayerRole_BackInput();
        roleDomain.PlayerRole_AnimWeaponToPos();

        // ========= Logic
        TickResTime(dt);
        var tickCount = PickRealTickCount();
        for (int i = 0; i < tickCount; i++) {
            if (state == GameFSMState.Lobby) {
                TickLobbyLogic(gameEntity, dt);
            } else if (state == GameFSMState.Battle) {
                TickBattleLogic(gameEntity, dt);
            }

            TickAnyLogic(gameEntity, dt);
        }


        // ========= Renderer
        roleDomain.EasingRenderer(dt);
    }

    public void TickAnyLogic(GameEntity gameEntity, float dt) {
    }

    public void TickLobbyLogic(GameEntity gameEntity, float dt) {

    }

    public void TickBattleLogic(GameEntity gameEntity, float dt) {
        var roleDomain = rootDomain.roleDomain;
        var roleFSMDomain = rootDomain.roleFSMDomain;
        var phxDomain = rootDomain.phxDomain;

        var stateModel = gameEntity.FSMCom.BattleStateModel;
        if (stateModel.IsEntering) {
            stateModel.SetIsEntering(false);
            roleDomain.TrySpawnPlayerRole();
            phxDomain.SetGlobalGravity(new Vector2(0, -20f));
        }

        roleFSMDomain.TickFSM(dt);
        phxDomain.Tick(dt);
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