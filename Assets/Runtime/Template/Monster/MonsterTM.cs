using UnityEngine;

[System.Serializable]
public struct MonsterTM {

    [SerializeField] public int typeID;
    [SerializeField] public int hp;

    [SerializeField] public FallPattern fallPattern;
    [SerializeField] public float fallSpeed;

    [SerializeField] public Vector2 size;

    public bool isDeadSpawnChildren;
    public int deadSpawnChildrenTypeID;

    [SerializeField] public GameObject bodyMod;

}
