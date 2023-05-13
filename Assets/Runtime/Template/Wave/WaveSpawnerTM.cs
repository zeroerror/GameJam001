using UnityEngine;

[System.Serializable]
public struct WaveSpawnerTM {

    [SerializeField][Header("生成所属波数")] public int waveID;
    [SerializeField][Header("是否为最后一个")] public bool isWaveEnd;
    [SerializeField][Header("生成时间点")] public float spawnTime;
    [SerializeField][Header("生成怪物类型ID")] public int typeID;

}