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
        var curWaveIndex = gameEntity.curWaveIndex;
        gameEntity.ForeachWaveSpawnerModel(stateModel.curTime, (spawnModel) => {
            var monsterDomain = rootDomain.monsterDomain;
            monsterDomain.TrySpawnMonster(spawnModel, out var monster);
        });

        // Wave Pause
        if (gameEntity.wavePaused) {
            // 检测当前波敌人均已死亡
            var monsterRepo = mainContext.rootRepo.monsterRepo;
            if (!monsterRepo.HasAliveMonster()) {
                gameEntity.wavePaused = false;
                gameEntity.hasWaveUpgrade = true;
                Debug.Log($"当前波次敌人消灭完成:{gameEntity.curWaveIndex}");
            }
        }

        if (!gameEntity.wavePaused) {
            stateModel.curTime += dt;
        }

        if (gameEntity.hasWaveUpgrade) {
            gameEntity.hasWaveUpgrade = false;
            // 升级选择
            var randomUpgradeType1 = GetRandomUpgradeTypeExcept(UpgradeType.None);
            var randomUpgradeType2 = GetRandomUpgradeTypeExcept(randomUpgradeType1);
            var randomUpgradeType3 = GetRandomUpgradeTypeExcept(randomUpgradeType2);
            Debug.Log($"升级选择 {randomUpgradeType1} {randomUpgradeType2} {randomUpgradeType3} ");
            var weaponForm1 = mainContext.rootRepo.weaponForm1;
            var weaponForm2 = mainContext.rootRepo.weaponForm2;
            var weaponForm3 = mainContext.rootRepo.weaponForm3;
            weaponForm1.LevelUp();
            weaponForm2.LevelUp();
            weaponForm3.LevelUp();
        }
    }

    public void Enter_Lobby(GameEntity Game) {
        var fsmCom = Game.FSMCom;
        fsmCom.EnterLobby();
    }

    public void Enter_Battle(GameEntity Game) {
        var fsmCom = Game.FSMCom;
        fsmCom.EnterBattle();
    }

    UpgradeType GetRandomUpgradeTypeExcept(UpgradeType type) {
        var upgradeTM = mainContext.rootTemplate.upgradeTM;
        var len = upgradeTM.upgradeTypeArray.Length;
        int randomIndex = Random.Range(0, len);
        while (type == upgradeTM.upgradeTypeArray[randomIndex]) {
            randomIndex = Random.Range(0, len);
        }

        return upgradeTM.upgradeTypeArray[randomIndex];
    }

}