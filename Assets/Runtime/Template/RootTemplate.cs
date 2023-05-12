using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTemplate {

    public RoleTemplate roleTemplate;
    public BulletTemplate bulletTemplate;
    public MonsterTemplate monsterTemplate;

    public RootTemplate() {
        roleTemplate = new RoleTemplate();
        bulletTemplate = new BulletTemplate();
        monsterTemplate = new MonsterTemplate();
    }

}
