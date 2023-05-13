using UnityEngine;

[CreateAssetMenu(fileName = "so_weaponform_", menuName = "Template/武器库模板")]
public class WeaponFormSO : ScriptableObject {

    [SerializeField] public WeaponFormTM tm;

}
