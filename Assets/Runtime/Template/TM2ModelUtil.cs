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
        model.waveID = tm.waveID;
        model.isWaveEnd = tm.isWaveEnd;
        model.spawnTime = tm.spawnTime;
        model.typeID = tm.typeID;
        return model;
    }

    public static WeaponFormAttrModel GetWeaponFormAttrModel(WeaponFormUpgradeTM tm){
        WeaponFormAttrModel model  =new WeaponFormAttrModel();
        model.bulletDamage = tm.bulletDamage;
        model.ammoCapacity = tm.ammoCapacity;
        model.bulletSize = tm.bulletSize;
        model.shootCD = tm.shootCD;
        model.reloadCD = tm.reloadCD;
        model.bloodThirst = tm.bloodThirst;
        model.fanOut = tm.fanOut;
        model.slow = tm.slow;
        model.hitBackDis = tm.hitBackDis;
        return model;
    }

}
