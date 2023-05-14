using System;
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
        monsterRepo.ForeachAll((monster) => {
            if (monster.FSMCom.State == MonsterFSMState.None) {
                return;
            }

            TickFSM(monster, dt);
        });
    }

    void TickFSM(MonsterEntity monster, float dt) {
        var fsmCom = monster.FSMCom;
        var state = fsmCom.State;
        if (state == MonsterFSMState.None) {
            return;
        }

        var hp = monster.HP;
        if (hp <= 0 && monster.FSMCom.State != MonsterFSMState.Dying) {
            Enter_Dying(monster);
        }

        if (state == MonsterFSMState.Falling) {
            TickFalling(monster, dt);
        } else if (state == MonsterFSMState.Dying) {
            TickDying(monster, dt);
        }

        monster.monsterShield.ClearFrameBlockBulletList();
    }

    public void TickFalling(MonsterEntity monster, float dt) {
        var fsmCom = monster.FSMCom;
        var model = fsmCom.FallingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
            // monster.SetFallVelocity(dt);
        }
        monster.Fall(dt);

        // ================== EXIT CHECK
    }

    public void TickDying(MonsterEntity monster, float dt) {
        var fsmCom = monster.FSMCom;
        var model = fsmCom.DyingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);

            if (monster.isDeadSpawnChildren) {
                Span<Vector2> randomPosArray = stackalloc Vector2[6];
                float gap = 1.2f;
                randomPosArray[0] = new Vector2(-gap * 0.77f, -gap);
                randomPosArray[1] = new Vector2(gap * 0.77f, -gap);
                randomPosArray[2] = new Vector2(gap * 2, 0);
                randomPosArray[3] = new Vector2(gap * 0.77f, gap);
                randomPosArray[4] = new Vector2(-gap * 0.77f, gap);
                randomPosArray[5] = new Vector2(-gap * 2, 0);
                for (int i = 0; i < randomPosArray.Length; i += 1) {
                    var rdPos = randomPosArray[i];
                    bool has = monsterDomain.SpawnMonster(monster.deadSpawnChildrenTypeID, rdPos, out var child);
                    if (has) {
                        child.SetPos((Vector2)monster.LogicPos + new Vector2(-1f, -1f) + rdPos);
                    }
                }
            }
            monster.TearDown();
        }

        // ================== Exit 
        fsmCom.Exit();
    }

    public void Enter_Falling(MonsterEntity Monster) {
        var fsmCom = Monster.FSMCom;
        fsmCom.EnterFalling();
        Debug.Log("MonsterFSM: ======> Enter_Falling");
    }

    public void Enter_Dying(MonsterEntity Monster) {
        var fsmCom = Monster.FSMCom;
        fsmCom.EnterDying();
        Debug.Log("MonsterFSM: ======> Enter_Dying");
    }

}