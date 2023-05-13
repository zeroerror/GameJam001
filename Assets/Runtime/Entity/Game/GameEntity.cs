
public class GameEntity {

    GameFSMComponent fsmCom;
    public GameFSMComponent FSMCom => fsmCom;

    WaveSpawnerModel[] waveSpawnerModelArray;
    public WaveSpawnerModel[] WaveSpawnerModelArray => waveSpawnerModelArray;
    public void SetWaveSpawnerModelArray(WaveSpawnerModel[] waveSpawnerModelArray) => this.waveSpawnerModelArray = waveSpawnerModelArray;

    public GameEntity() {
        this.fsmCom = new GameFSMComponent();
    }

    public void ForeachWaveSpawnerModel(float curTime, System.Action<WaveSpawnerModel> action) {
        var len = waveSpawnerModelArray.Length;
        for (var i = 0; i < len; ++i) {
            var model = waveSpawnerModelArray[i];
            if (model.isSpawned) continue;
            if (curTime >= model.spawnTime) {
                action(model);
                model.isSpawned = true;
            }
        }
    }

}
