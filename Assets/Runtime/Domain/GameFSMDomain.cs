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
            GetThreeRandomUpgradeType(out var upgradeTM1, out var upgradeTM2, out var upgradeTM3);
            Debug.Log($"升级选择 {upgradeTM1} {upgradeTM2} {upgradeTM3} ");
            var weaponForm1 = mainContext.rootRepo.weaponForm1;
            var weaponForm2 = mainContext.rootRepo.weaponForm2;
            var weaponForm3 = mainContext.rootRepo.weaponForm3;
            UpgradeWeaponForm(weaponForm1, upgradeTM1);
            UpgradeWeaponForm(weaponForm2, upgradeTM2);
            UpgradeWeaponForm(weaponForm3, upgradeTM3);
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

    void GetThreeRandomUpgradeType(out UpgradeTM upgradeType1, out UpgradeTM upgradeType2, out UpgradeTM upgradeType3) {
        upgradeType1 = GetRandomUpgradeTypeExcept(UpgradeType.None);
        upgradeType2 = GetRandomUpgradeTypeExcept(upgradeType1.upgradeType);
        upgradeType3 = GetRandomUpgradeTypeExcept(upgradeType2.upgradeType);
    }

    UpgradeTM GetRandomUpgradeTypeExcept(UpgradeType type) {
        var upgradeTMArray = mainContext.rootTemplate.upgradeTMArray;
        var len = upgradeTMArray.Length;
        int randomIndex = Random.Range(0, len);

        int count = 0;
        while (type == upgradeTMArray[randomIndex].upgradeType) {
            randomIndex = Random.Range(0, len);
            count++;
            if (count > 100) {
                break;
            }
        }

        return upgradeTMArray[randomIndex];
    }

    void UpgradeWeaponForm(WeaponFormEntity weaponForm, UpgradeTM upgradeTM) {
        weaponForm.LevelUp();
        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var upgradeType = upgradeTM.upgradeType;

        // - WeaponForm
        if (upgradeType == UpgradeType.AmmoCapacity) {
            weaponForm.AttrModel.bulletCapacity += (int)upgradeTM.factor;
            Debug.Log($"武器弹夹升级 {weaponForm.AttrModel.bulletCapacity}");
            return;
        }

        if (upgradeType == UpgradeType.ShootCD) {
            weaponForm.AttrModel.shootCD += upgradeTM.factor;
            Debug.Log($"武器射击升级 {weaponForm.AttrModel.shootCD}");
            return;
        }

        if (upgradeType == UpgradeType.ReloadCD) {
            weaponForm.AttrModel.reloadCD += upgradeTM.factor;
            Debug.Log($"武器装填升级 {weaponForm.AttrModel.reloadCD}");
            return;
        }

        // - Bullet        
        if (upgradeType == UpgradeType.BloodThirst) {
            weaponForm.AttrModel.bulletModel.bloodThirst += (int)upgradeTM.factor;
            Debug.Log($"子弹吸血升级 {weaponForm.AttrModel.bulletModel.bloodThirst}");
            return;
        }

        if (upgradeType == UpgradeType.FanOut) {
            weaponForm.AttrModel.bulletModel.fanOut += (int)upgradeTM.factor;
            Debug.Log($"子弹扇形升级 {weaponForm.AttrModel.bulletModel.fanOut}");
            return;
        }

        if (upgradeType == UpgradeType.Slow) {
            weaponForm.AttrModel.bulletModel.slow += upgradeTM.factor;
            Debug.Log($"子弹减速升级 {weaponForm.AttrModel.bulletModel.slow}");
            return;
        }

        if (upgradeType == UpgradeType.HitBack) {
            weaponForm.AttrModel.bulletModel.hitBackDis += upgradeTM.factor;
            Debug.Log($"子弹击退升级 {weaponForm.AttrModel.bulletModel.hitBackDis}");
            return;
        }

        if (upgradeType == UpgradeType.BulletDamage) {
            // 百分比
            var damage_base = weaponForm.AttrModel.bulletModel.bulletDamage_base;
            var addDamage = damage_base * upgradeTM.factor;
            weaponForm.AttrModel.bulletModel.bulletDamage += (int)addDamage;
            Debug.Log($"子弹伤害升级 {weaponForm.AttrModel.bulletModel.bulletDamage}");
            return;
        }

        if (upgradeType == UpgradeType.BulletSize) {
            var size_base = weaponForm.AttrModel.bulletModel.bulletSize_base;
            weaponForm.AttrModel.bulletModel.bulletSize += upgradeTM.factor * size_base;
            Debug.Log($"子弹大小升级 {weaponForm.AttrModel.bulletModel.bulletSize}");
            return;
        }
    }

}