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
        if (state == GameFSMState.Lobby) {
            TickLobbyLogic(gameEntity, dt);
        } else if (state == GameFSMState.Battle) {
            TickBattle(gameEntity, dt);
        }
        TickAnyLogic(gameEntity, dt);

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
        var weaponFormFSMDomain = rootDomain.weaponFormFSMDomain;
        var phxDomain = rootDomain.phxDomain;
        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;

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
            gameEntity.baseHP = globalConfigTM.baseHP;
            gameEntity.baseMaxHP = globalConfigTM.baseHP;
        }

        monsterFSMDomain.TickFSM(dt);

        roleFSMDomain.TickFSM(dt);
        bulletFSMDomain.TickFSM(dt);
        weaponFormFSMDomain.TickFSM(dt);
        phxDomain.Tick(dt);

        // Wave Control
        var curWaveIndex = gameEntity.curWaveIndex;
        gameEntity.ForeachWaveSpawnerModel(stateModel.curTime, (spawnModel) => {
            var monsterDomain = rootDomain.monsterDomain;
            monsterDomain.TrySpawnMonster(spawnModel, out var monster);
        });

        // 波暂停逻辑 在这里-----------------------------------------------------------------
        if (gameEntity.WavePaused) {
            var monsterRepo = mainContext.rootRepo.monsterRepo;
            if (!monsterRepo.HasAliveMonster() && !gameEntity.hasWaveUpgrade) {
                Debug.Log($"当前波次敌人消灭完成:{gameEntity.curWaveIndex}");

                // 放到UI回调里面
                gameEntity.ContinueWave();
                gameEntity.hasWaveUpgrade = true;
                stateModel.curTime = 0f;

            }
        }

        if (!gameEntity.WavePaused) {
            stateModel.curTime += dt;
        }

        // 升级逻辑 在这里-----------------------------------------------------------------
        if (gameEntity.hasWaveUpgrade) {
            gameEntity.hasWaveUpgrade = false;
            // 升级选择
            GetThreeRandomUpgradeType(out var upgradeType1, out var upgradeType2, out var upgradeType3);
            Debug.Log($"升级选择 {upgradeType1} {upgradeType2} {upgradeType3} ");
            var weaponForm1 = mainContext.rootRepo.weaponForm1;
            var weaponForm2 = mainContext.rootRepo.weaponForm2;
            var weaponForm3 = mainContext.rootRepo.weaponForm3;
            UpgradeWeaponForm(weaponForm1, upgradeType1);
            UpgradeWeaponForm(weaponForm2, upgradeType2);
            UpgradeWeaponForm(weaponForm3, upgradeType3);
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

    void GetThreeRandomUpgradeType(out UpgradeType upgradeType1, out UpgradeType upgradeType2, out UpgradeType upgradeType3) {
        upgradeType1 = GetRandomUpgradeTypeExcept(UpgradeType.None);
        upgradeType2 = GetRandomUpgradeTypeExcept(upgradeType1);
        upgradeType3 = GetRandomUpgradeTypeExcept(upgradeType2);
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

    void UpgradeWeaponForm(WeaponFormEntity weaponForm, UpgradeType upgradeType) {
        weaponForm.LevelUp();
        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var weaponFormUpgradeTM = globalConfigTM.weaponFormUpgradeTM;
        var bulletTM = weaponFormUpgradeTM.bulletTM;

        // - WeaponForm
        if (upgradeType == UpgradeType.AmmoCapacity) {
            weaponForm.AttrModel.bulletCapacity += weaponFormUpgradeTM.ammoCapacity;
            return;
        }

        if (upgradeType == UpgradeType.ShootCD) {
            weaponForm.AttrModel.shootCD += weaponFormUpgradeTM.shootCD;
            return;
        }

        if (upgradeType == UpgradeType.ReloadCD) {
            weaponForm.AttrModel.reloadCD += weaponFormUpgradeTM.reloadCD;
            return;
        }

        // - Bullet        
        if (upgradeType == UpgradeType.BloodThirst) {
            weaponForm.AttrModel.bulletModel.bloodThirst += bulletTM.bloodThirst;
            return;
        }

        if (upgradeType == UpgradeType.FanOut) {
            weaponForm.AttrModel.bulletModel.fanOut += bulletTM.fanOut;
            return;
        }

        if (upgradeType == UpgradeType.Slow) {
            weaponForm.AttrModel.bulletModel.slow += bulletTM.slow;
            return;
        }

        if (upgradeType == UpgradeType.HitBack) {
            weaponForm.AttrModel.bulletModel.hitBackDis += bulletTM.hitBackDis;
            return;
        }

        if (upgradeType == UpgradeType.BulletDamage) {
            weaponForm.AttrModel.bulletModel.bulletDamage += bulletTM.bulletDamage;
            return;
        }

        if (upgradeType == UpgradeType.BulletSize) {
            weaponForm.AttrModel.bulletModel.bulletSize += bulletTM.bulletSize;
            return;
        }
    }

}