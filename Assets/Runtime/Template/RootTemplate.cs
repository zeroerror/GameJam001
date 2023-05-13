using UnityEngine;

public class RootTemplate {

    public RoleTemplate roleTemplate;
    public BulletTemplate bulletTemplate;
    public MonsterTemplate monsterTemplate;
    public WaveTemplate waveTemplate;

    public GlobalConfigTM globalConfigTM;

    public RootTemplate() {
        roleTemplate = new RoleTemplate();
        bulletTemplate = new BulletTemplate();
        monsterTemplate = new MonsterTemplate();
        waveTemplate = new WaveTemplate();

        this.globalConfigTM = Resources.LoadAll<GlobalConfigSO>("Config")[0].tm;
    }

}
