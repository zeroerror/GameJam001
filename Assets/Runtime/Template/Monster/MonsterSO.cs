using UnityEngine;

[CreateAssetMenu(fileName = "so_monster_", menuName = "Template/怪物模板")]
public class MonsterSO : ScriptableObject {

    [SerializeField] public MonsterTM tm;

}
