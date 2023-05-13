using UnityEngine;

[CreateAssetMenu(fileName = "so_bullet_", menuName = "Template/子弹模板")]
public class BulletSO : ScriptableObject {

    [SerializeField] public BulletTM tm;

}
