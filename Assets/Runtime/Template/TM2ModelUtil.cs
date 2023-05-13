using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TM2ModelUtil {

    public static WaveModel GetWaveModel(WaveTM tm) {
        WaveModel waveModel;
        waveModel.waveSpawnerModelArray = GetWaveSpawnerModelArray(tm.waveSpawnerTMArray);
        return waveModel;
    }

    public static WaveSpawnerModel[] GetWaveSpawnerModelArray(WaveSpawnerTM[] tmArray) {
        if (tmArray == null) return null;

        WaveSpawnerModel[] modelArray = new WaveSpawnerModel[tmArray.Length];
        for (int i = 0; i < tmArray.Length; i++) {
            modelArray[i] = GetWaveSpawnerModel(tmArray[i]);
        }
        return modelArray;
    }

    public static WaveSpawnerModel GetWaveSpawnerModel(WaveSpawnerTM tm) {
        WaveSpawnerModel model = new WaveSpawnerModel();
        model.spawnTime = tm.spawnTime;
        model.typeID = tm.typeID;
        return model;

    }

}
