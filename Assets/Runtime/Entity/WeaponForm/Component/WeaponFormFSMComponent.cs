using UnityEngine;

public class WeaponFormFSMComponent {

    WeaponFormFSMState state;
    public WeaponFormFSMState State => state;

    WeaponFSMStateModel_Idle idleStateModel;
    public WeaponFSMStateModel_Idle IdleStateModel => idleStateModel;

    WeaponFSMStateModel_Reloading reloadingStateModel;
    public WeaponFSMStateModel_Reloading ReloadingStateModel => reloadingStateModel;

    WeaponFSMStateModel_Shooting shootingStateModel;
    public WeaponFSMStateModel_Shooting ShootingStateModel => shootingStateModel;

    public WeaponFormFSMComponent() {
        state = WeaponFormFSMState.None;
        this.idleStateModel = new WeaponFSMStateModel_Idle();
        this.reloadingStateModel = new WeaponFSMStateModel_Reloading();
        this.shootingStateModel = new WeaponFSMStateModel_Shooting();
    }

    public void Reset() {
        state = WeaponFormFSMState.None;
        idleStateModel.Reset();
        reloadingStateModel.Reset();
        shootingStateModel.Reset();
    }

    public void EnterIdle() {
        var model = idleStateModel;
        model.Reset();
        model.SetIsEntering(true);
        state = WeaponFormFSMState.Idle;
    }

    public void EnterReloading() {
        var model = reloadingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        state = WeaponFormFSMState.Reloading;
    }

    public void EnterShooting() {
        var model = shootingStateModel;
        model.Reset();
        model.SetIsEntering(true);
        state = WeaponFormFSMState.Shooting;
    }

}