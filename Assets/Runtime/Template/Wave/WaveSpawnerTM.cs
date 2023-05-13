using UnityEngine;

[System.Serializable]
public struct WaveSpawnerTM {

    [SerializeField][Header("生成时间点")] public float spawnTime;
    [SerializeField][Header("生成怪物类型ID")] public int typeID;

}