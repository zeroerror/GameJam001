using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTemplate {

    public RoleTemplate roleTemplate;
    public WeaponFormTemplate bulletTemplate;
    public MonsterTemplate monsterTemplate;

    public RootTemplate() {
        roleTemplate = new RoleTemplate();
        bulletTemplate = new WeaponFormTemplate();
        monsterTemplate = new MonsterTemplate();
    }

}
