using UnityEngine;

public class MonsterDomain {

    MainContext mainContext;
    Factory factory;
    MonsterFSMDomain monsterFSMDomain;

    public void Inject(MainContext mainContext, Factory factory, MonsterFSMDomain monsterFSMDomain) {
        this.mainContext = mainContext;
        this.factory = factory;
        this.monsterFSMDomain = monsterFSMDomain;
    }

    public bool TrySpawnMonster(WaveSpawnerModel spawnerModel, out MonsterEntity monster) {
        var typeID = spawnerModel.typeID;
        if (!factory.TryCreateMonster(typeID, out monster)) {
            Debug.LogError($"创建怪物失败 {typeID}");
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickMonsterID();
        monster.IDCom.SetEntityID(id);

        monsterFSMDomain.Enter_Falling(monster);

        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var xRange = globalConfigTM.monsterSpawnPosXRange;
        var posX = Random.Range(xRange.x, xRange.y);
        var posY = globalConfigTM.monsterSpawnPosY;
        monster.SetPos(new Vector2(posX, posY));

        var monsterRepo = mainContext.rootRepo.monsterRepo;
        monsterRepo.TryAdd(monster);

        return true;
    }

    public void EasingRenderer(float dt) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        monsterRepo.ForeachAll((monster) => {
            monster.EasingToDstPos(dt);
        });
    }

}