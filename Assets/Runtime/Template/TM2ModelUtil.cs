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

    public static WeaponFormAttrModel GetWeaponFormAttrModel(GlobalConfigTM tm) {
        WeaponFormAttrModel model = new WeaponFormAttrModel();
        model.bulletCapacity = tm.bulletCapacity_init;
        model.shootCD = tm.shootCD_init;
        model.reloadCD = tm.reloadCD_init;
        return model;
    }

    public static BulletModel GetBulletModel(BulletTM tm) {
        BulletModel model;

        model.bulletType = tm.bulletType;

        model.bulletSize = tm.bulletSize;
        model.bulletDamage = tm.bulletDamage;
        model.bloodThirst = tm.bloodThirst;
        model.fanOut = tm.fanOut;
        model.slow = tm.slow;
        model.hitBackDis = tm.hitBackDis;
        model.flySpeed = tm.flySpeed;

        model.bulletSize_base = tm.bulletSize;
        model.bulletDamage_base = tm.bulletDamage;
        model.bloodThirst_base = tm.bloodThirst;
        model.fanOut_base = tm.fanOut;
        model.slow_base = tm.slow;
        model.hitBackDis_base = tm.hitBackDis;
        model.flySpeed_base = tm.flySpeed;
        model.bulletIcon = tm.bulletIcon;
        model.weaponIcon = tm.weaponIcon;
        model.themeColor = tm.themeColor;
        
        return model;
    }

}
