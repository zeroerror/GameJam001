using UnityEngine;

public class WeaponFormFSMDomain {

    MainContext mainContext;
    WeaponFormDomain weaponFormDomain;

    public void Inject(MainContext mainContext, WeaponFormDomain weaponFormDomain) {
        this.mainContext = mainContext;
        this.weaponFormDomain = weaponFormDomain;
    }

    public void TickFSM(float dt) {
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
            TickIdle(weaponForm, dt);
        } else if (state == WeaponFormFSMState.Shooting) {
            TickIdle(weaponForm, dt);
        }

        TickAny(weaponForm, dt);
    }

    public void TickAny(WeaponFormEntity weaponForm, float dt) {
        var hp = weaponForm.AttrModel.hp;
        if (hp <= 0) {
            Enter_Shooting(weaponForm);
        }
    }

    public void TickIdle(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.IdleStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // ================== EXIT CHECK
    }

    public void TickReloading(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.ReloadingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // ================== EXIT CHECK

    }

    public void TickShooting(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        var model = fsmCom.ShootingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // ================== EXIT CHECK

    }

    public void TickDying(WeaponFormEntity weaponForm, float dt) {
        var fsmCom = weaponForm.FSMCom;
        fsmCom.Exit();
        // ================== EXIT CHECK
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

    public void Enter_Shooting(WeaponFormEntity weaponForm) {
        var fsmCom = weaponForm.FSMCom;
        fsmCom.EnterShooting();
        Debug.Log($"WeaponFormFSM: ======> Enter_Shooting");
    }

    public void Enter_Dying(WeaponFormEntity weaponForm) {
        var fsmCom = weaponForm.FSMCom;
        fsmCom.EnterDying();
        Debug.Log($"WeaponFormFSM: ======> Enter_Dying");
    }

}