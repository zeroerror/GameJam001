using UnityEngine;

public class RootTemplate {

    public RoleTemplate roleTemplate;
    public MonsterTemplate monsterTemplate;
    public WaveTemplate waveTemplate;
    public BulletTemplate bulletTemplate    ;

    public GlobalConfigTM globalConfigTM;
    public UpgradeTM upgradeTM;

    public RootTemplate() {
        roleTemplate = new RoleTemplate();
        monsterTemplate = new MonsterTemplate();
        waveTemplate = new WaveTemplate();
        bulletTemplate = new BulletTemplate();

        this.globalConfigTM = Resources.LoadAll<GlobalConfigSO>("Config")[0].tm;
        this.upgradeTM = Resources.LoadAll<UpgradeSO>("Upgrade")[0].tm;
    }

}
