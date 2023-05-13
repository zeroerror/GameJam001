using UnityEngine;

public class GameFSMDomain {

    MainContext mainContext;
    RootDomain rootDomain;
    WeaponFormDomain weaponFormDomain;
    public void Inject(MainContext mainContext, WeaponFormDomain weaponFormDomain, RootDomain rootDomain) {
        this.mainContext = mainContext;
        this.weaponFormDomain = weaponFormDomain;
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

        var roleDomain = rootDomain.roleDomain;
        var monsterDomain = rootDomain.monsterDomain;
        var bulletDomain = rootDomain.bulletDomain;
        // ========= Input
        roleDomain.PlayerRole_BackInput();
        roleDomain.PlayerRole_AnimWeaponToPos();

        // ========= Logic
        TickResTime(dt);
        var tickCount = PickRealTickCount();
        for (int i = 0; i < tickCount; i++) {
            if (state == GameFSMState.Lobby) {
                TickLobbyLogic(gameEntity, dt);
            } else if (state == GameFSMState.Battle) {
                TickBattle(gameEntity, dt);
            }

            TickAnyLogic(gameEntity, dt);
        }


        // ========= Renderer
        roleDomain.EasingRenderer(dt);
        monsterDomain.EasingRenderer(dt);
        bulletDomain.EasingRenderer(dt);
    }

    public void TickAnyLogic(GameEntity gameEntity, float dt) {
    }

    public void TickLobbyLogic(GameEntity gameEntity, float dt) {

    }

    public void TickBattle(GameEntity gameEntity, float dt) {
        var roleDomain = rootDomain.roleDomain;
        var roleFSMDomain = rootDomain.roleFSMDomain;
        var monsterFSMDomain = rootDomain.monsterFSMDomain;
        var bulletFSMDomain = rootDomain.bulletFSMDomain;
        var phxDomain = rootDomain.phxDomain;

        var stateModel = gameEntity.FSMCom.BattleStateModel;
        if (stateModel.IsEntering) {
            stateModel.SetIsEntering(false);
            phxDomain.SetGlobalGravity(new Vector2(0, -20f));

            roleDomain.TrySpawnPlayerRole();
            weaponFormDomain.TrySpawnWeaponFormThree();

            var waveTemplate = mainContext.rootTemplate.waveTemplate;
            waveTemplate.TryGetWaveTM(out var tm);
            var waveModel = TM2ModelUtil.GetWaveModel(tm);
            gameEntity.SetWaveSpawnerModelArray(waveModel.waveSpawnerModelArray);
        }

        monsterFSMDomain.TickFSM(dt);
        roleFSMDomain.TickFSM(dt);
        bulletFSMDomain.TickFSM(dt);
        phxDomain.Tick(dt);

        // Wave Control
        gameEntity.ForeachWaveSpawnerModel(stateModel.curTime, (spawnModel) => {
            var monsterDomain = rootDomain.monsterDomain;
            monsterDomain.TrySpawnMonster(spawnModel, out var monster);
            Debug.Log($"生成怪物 {spawnModel.typeID} ");
        });
        stateModel.curTime += dt;
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