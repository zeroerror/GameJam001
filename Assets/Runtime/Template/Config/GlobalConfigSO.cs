using UnityEngine;

[CreateAssetMenu(fileName = "so_config_global_", menuName = "Template/全局配置模板")]
public class GlobalConfigSO : ScriptableObject {

    [SerializeField] public GlobalConfigTM tm;

}
