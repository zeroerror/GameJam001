using UnityEngine;

public class BulletFSMDomain {

    MainContext mainContext;

    public void Inject(MainContext mainContext) {
        this.mainContext = mainContext;
    }

    public void TickFSM(float dt) {
        var bulletRepo = mainContext.rootRepo.bulletRepo;
        bulletRepo.ForeachAll((bullet) => {
            TickFSM(bullet, dt);
        });
    }

    void TickFSM(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var state = fsmCom.State;
        if (state == BulletFSMState.None) {
            return;
        }

        if (state == BulletFSMState.Flying) {
            TickFlying(bullet, dt);
        } else if (state == BulletFSMState.Exploding) {
            TickExploding(bullet, dt);
        }

        TickAny(bullet, dt);
    }

    public void TickAny(BulletEntity bullet, float dt) {
    }

    public void TickFlying(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var model = fsmCom.FlyingStateModel;

        var flyDir = model.FlyDir;
        if (model.IsEntering) {
            model.SetIsEntering(false);
            bullet.Fly(flyDir * bullet.flySpeed);
        }

        model.time += dt;

        // ================== Exit 
        var globalConfigTM = mainContext.rootTemplate.globalConfigTM;
        var bulletMaxFlyTime = globalConfigTM.bulletMaxFlyTime!;
        var explodeRadius = globalConfigTM.rocketExplodeRadius;
        if (model.time > bulletMaxFlyTime) {
            Enter_Exploding(bullet, explodeRadius);
            return;
        }
    }

    public void TickExploding(BulletEntity bullet, float dt) {
        var fsmCom = bullet.FSMCom;
        var model = fsmCom.ExplodingStateModel;

        if (model.IsEntering) {
            model.SetIsEntering(false);
            var bulletType = bullet.bulletType;
            if (bulletType == BulletType.Rocket) {
                var explodeRadius = model.ExplodeRadius;
                var monsterRepo = mainContext.rootRepo.monsterRepo;
                monsterRepo.ForeachAll((monster) => {
                    if (monster.isNotValid) return;
                    var monsterPos = monster.LogicPos;
                    var bulletPos = bullet.LogicPos;
                    var distance = Vector2.Distance(monsterPos, bulletPos);
                    if (distance < explodeRadius) {
                        var damage = bullet.bulletDamage;
                        var clampHP = System.Math.Clamp(monster.HP - damage, 0, int.MaxValue);
                        monster.SetHP(clampHP);
                    }
                });

            }
        }

        // ================== Exit 
        bullet.TearDown();
        fsmCom.Exit();
    }

    public void Enter_Flying(BulletEntity bullet, Vector2 flyDir) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterFlying(flyDir);
        Debug.Log($"BulletFSM: ======> Enter_Flying dir:{flyDir}");
    }

    public void Enter_Exploding(BulletEntity bullet, float explodeRadius) {
        var fsmCom = bullet.FSMCom;
        fsmCom.EnterExploding(explodeRadius);
        Debug.Log("BulletFSM: ======> Enter_Exploding 爆炸半径:" + explodeRadius);
    }


}