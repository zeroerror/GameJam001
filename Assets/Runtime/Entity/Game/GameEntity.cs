using UnityEngine;

public class GameEntity {

    GameFSMComponent fsmCom;
    public GameFSMComponent FSMCom => fsmCom;

    WaveSpawnerModel[] waveSpawnerModelArray;
    public WaveSpawnerModel[] WaveSpawnerModelArray => waveSpawnerModelArray;
    public void SetWaveSpawnerModelArray(WaveSpawnerModel[] waveSpawnerModelArray) => this.waveSpawnerModelArray = waveSpawnerModelArray;

    public int curWaveIndex;

    bool wavePaused;
    public bool WavePaused => wavePaused;
    public void ContinueWave() {
        wavePaused = false;
        
    }

    public bool hasWaveUpgrade;

    public int baseHP;
    public int baseMaxHP;

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

                var waveID = model.waveID;
                if (waveID > curWaveIndex) {
                    curWaveIndex = waveID;
                    Debug.Log($"当前波次:{curWaveIndex}");
                }

                if (model.isWaveEnd) {
                    wavePaused = true;
                    Debug.Log($"当前波次生成结束:{curWaveIndex}");
                }
            }


        }
    }

}
