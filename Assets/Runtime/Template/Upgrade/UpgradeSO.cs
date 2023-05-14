using UnityEngine;

[CreateAssetMenu(fileName = "so_upgrade_", menuName = "Template/升级模板")]
public class UpgradeSO : ScriptableObject {

    [SerializeField] public UpgradeTM[] tmArray;

}
