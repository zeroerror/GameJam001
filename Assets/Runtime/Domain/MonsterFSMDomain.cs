using UnityEngine;

public class MonsterFSMDomain {

    MainContext mainContext;
    MonsterDomain monsterDomain;

    public void Inject(MainContext mainContext, MonsterDomain monsterDomain) {
        this.mainContext = mainContext;
        this.monsterDomain = monsterDomain;
    }

    public void TickFSM(float dt) {
        var monsterRepo = mainContext.rootRepo.monsterRepo;
        monsterRepo.ForeachAll((Monster) => {
            TickFSM(Monster, dt);
        });
    }

    void TickFSM(MonsterEntity Monster, float dt) {
        var fsmCom = Monster.FSMCom;
        var state = fsmCom.State;
        if (state == MonsterFSMState.None) {
            return;
        }

        if (state == MonsterFSMState.Falling) {
            TickFalling(Monster, dt);
        } else if (state == MonsterFSMState.Dying) {
            TickDying(Monster, dt);
        }

        TickAny(Monster, dt);
    }

    public void TickAny(MonsterEntity Monster, float dt) {
    }

    public void TickFalling(MonsterEntity monster, float dt) {
        var fsmCom = monster.FSMCom;
        var model = fsmCom.FallingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
            monster.SetFallVelocity(dt);
        }

        // ================== EXIT CHECK
    }

    public void TickDying(MonsterEntity Monster, float dt) {
        var fsmCom = Monster.FSMCom;
        var model = fsmCom.DyingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
        }

        // ================== Exit 
    }

    public void Enter_Falling(MonsterEntity Monster) {
        var fsmCom = Monster.FSMCom;
        fsmCom.EnterFalling();
        Debug.Log("MonsterFSM: ======> Enter_Falling");
    }

    public void Enter_Dying(MonsterEntity Monster) {
        var fsmCom = Monster.FSMCom;
        fsmCom.EnterDying();
        Debug.Log("MonsterFSM: ======> Enter_Moving");
    }

}