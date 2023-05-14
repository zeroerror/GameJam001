using UnityEngine;

public class WeaponFormFSMDomain {

    MainContext mainContext;
    WeaponFormDomain weaponFormDomain;
    BulletDomain bulletDomain;
    BulletFSMDomain bulletFSMDomain;

    public void Inject(MainContext mainContext,
                       WeaponFormDomain weaponFormDomain,
                       BulletDomain bulletDomain,
                       BulletFSMDomain bulletFSMDomain) {
        this.mainContext = mainContext;
        this.weaponFormDomain = weaponFormDomain;
        this.bulletDomain = bulletDomain;
        this.bulletFSMDomain = bulletFSMDomain;
    }

    public void TickFSM(float dt) {
        var weaponForm1 = mainContext.rootRepo.weaponForm1;
        var weaponForm2 = mainContext.rootRepo.weaponForm2;
        var weaponForm3 = mainContext.rootRepo.weaponForm3;
        TickFSM(weaponForm1, dt);
        TickFSM(weaponForm2, dt);
        TickFSM(weaponForm3, dt);
    }

    void TickFSM(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var state = fsmCom.State;
        if (state == WeaponFormFSMState.None) {
            return;
        }

        if (state == WeaponFormFSMState.Idle) {
            TickIdle(weaponForm, dt);
        } else if (state == WeaponFormFSMState.Reloading) {
            TickReloading(weaponForm, dt);
        } else if (state == WeaponFormFSMState.Shooting) {
            TickShooting(weaponForm, dt);
        }

        TickAny(weaponForm, dt);
    }

    public void TickAny(WeaponFormEntity weaponForm, float dt) {
    }

    public void TickIdle(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.IdleStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        model.time += dt;

        // ================== EXIT CHECK
        // 闲置 2s 后自动进入装填状态
        var cd = mainContext.rootTemplate.globalConfigTM.weaponFormIdleToReloadCD;
        if (model.time >= cd
        && weaponForm.curBulletCount < weaponForm.AttrModel.bulletCapacity) {
            Enter_Reloading(weaponForm);
        }
    }

    public void TickReloading(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.ReloadingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        model.time += dt;

        var attrModel = weaponForm.AttrModel;
        var reloadCD = attrModel.reloadCD;

        if (model.time >= reloadCD) {
            var count = weaponForm.curBulletCount + 1;
            count = count > attrModel.bulletCapacity ? attrModel.bulletCapacity : count;
            weaponForm.curBulletCount = count;
            model.time = 0;
        }

        // ================== EXIT CHECK
    }

    public void TickShooting(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.ShootingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // 弹药检查
        if (weaponForm.curBulletCount <= 0) {
            Enter_Idle(weaponForm);
            return;
        }

        model.time += dt;
        model.triplet_time += dt;

        var bulletType = weaponForm.BulletType;
        var attrModel = weaponForm.AttrModel;
        var shootTarPos = model.ShootTargetPos;

        // ================== EXIT CHECK
        if (bulletType == BulletType.Normal) {
            // 普通子弹 射一发就退出
            if (model.triplet_count < 1) {
                weaponFormDomain.Shoot(weaponForm, shootTarPos);
                model.triplet_count++;
            }

            if (model.time >= attrModel.shootCD) {
                Enter_Idle(weaponForm);
            }

        } else if (bulletType == BulletType.Laser) {
            // 激光  
            if (model.triplet_count < 1) {
                weaponFormDomain.Shoot(weaponForm, shootTarPos);
                model.triplet_count++;
            }

            if (model.time >= attrModel.shootCD) {
                Enter_Idle(weaponForm);
            }
        } else if (bulletType == BulletType.Triplet) {
            // 三连发 射3发就退出
            if (model.triplet_count >= 3
            && model.time >= attrModel.shootCD) {
                Enter_Idle(weaponForm);
            } else {
                if (model.triplet_time >= 0.16f
                && model.triplet_count < 3) {
                    model.triplet_time = 0;
                    weaponFormDomain.Shoot(weaponForm, shootTarPos);
                    model.triplet_count++;
                    Debug.Log($"三连发");
                }
            }

        } else if (bulletType == BulletType.Rocket) {
            // 火箭弹 射一发就退出
            if (model.triplet_count < 1) {
                weaponFormDomain.Shoot(weaponForm, shootTarPos);
                model.triplet_count++;
            }

            if (model.time >= attrModel.shootCD) {
                Enter_Idle(weaponForm);
            }
        } else if (bulletType == BulletType.Bubble) {
            // 气泡弹 射一发就退出
            if (model.triplet_count < 1) {
                weaponFormDomain.Shoot(weaponForm, shootTarPos);
                model.triplet_count++;
            }

            if (model.time >= attrModel.shootCD) {
                Enter_Idle(weaponForm);
            }
        } else {
            Debug.LogError($"未知的子弹类型 {bulletType}");
        }
    }

    public void Enter_Idle(WeaponFormEntity weaponForm) {
        var fsmCom = weaponForm.FSMCom;
        fsmCom.EnterIdle();
        Debug.Log($"WeaponFormFSM: ======> Enter_Idle");
    }

    public void Enter_Reloading(WeaponFormEntity weaponForm) {
        var fsmCom = weaponForm.FSMCom;
        fsmCom.EnterReloading();
        Debug.Log($"WeaponFormFSM: ======> Enter_Reloading");
    }

    public void Enter_Shooting(WeaponFormEntity weaponForm, Vector2 shootTarPos) {
        if (weaponForm.curBulletCount <= 0) {
            return;
        }

        var fsmCom = weaponForm.FSMCom;
        fsmCom.EnterShooting(shootTarPos);
        Debug.Log($"WeaponFormFSM: ======> Enter_Shooting {shootTarPos}");
    }


}