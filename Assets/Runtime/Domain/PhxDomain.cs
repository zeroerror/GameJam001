using UnityEngine;

public class PhxDomain {

    MainContext mainContext;
    RootDomain rootDomain;

    public void Inject(MainContext mainContext, RootDomain rootDomain) {
        this.mainContext = mainContext;
        this.rootDomain = rootDomain;
    }

    public void Tick(float dt) {
        Physics2D.Simulate(dt);

        // PHX EVENT
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.ForeachAllEnter((evModel) => {
            HandleEnter(evModel);
        });

        phxEventRepo.ForeachAllExit((evModel) => {
            HandleExit(evModel);
        });

        phxEventRepo.ClearAll();
    }

    public void SetGlobalGravity(Vector2 gravity) {
        Physics2D.gravity = gravity;
    }

    public void HandleEnter(in PhxEventModel evModel) {
        var monsterDomain = rootDomain.monsterDomain;
        var bulletDomain = rootDomain.bulletDomain;
        var weaponFormDomain = rootDomain.weaponFormDomain;

        var one = evModel.one;
        var two = evModel.two;
        if (one.entityType == EntityType.Monster && two.entityType == EntityType.Bullet) {
            monsterDomain.HandleBeHitByBullet(one, two);
            bulletDomain.HandleHitMonster(two, one);
            return;
        }
        if (one.entityType == EntityType.Bullet && two.entityType == EntityType.Monster) {
            monsterDomain.HandleBeHitByBullet(two, one);
            bulletDomain.HandleHitMonster(one, two);
            return;
        }

        if (one.entityType == EntityType.Monster && two.entityType == EntityType.WeaponForm) {
            monsterDomain.HandleHitWeaponForm(one, two);
            weaponFormDomain.HandleBeHitByMonster(two, one);
            return;
        }
        if (one.entityType == EntityType.WeaponForm && two.entityType == EntityType.Monster) {
            monsterDomain.HandleHitWeaponForm(two, one);
            weaponFormDomain.HandleBeHitByMonster(one, two);
            return;
        }

        if (one.entityType == EntityType.Bullet && LayerMask.LayerToName(evModel.layerMask_two) == "Wall") {
            bulletDomain.HandleHitWall(one, -evModel.normal);
            return;
        }
        if (LayerMask.LayerToName(evModel.layerMask_one) == "Wall" && two.entityType == EntityType.Bullet) {
            bulletDomain.HandleHitWall(two, evModel.normal);
            return;
        }
    }

    public void HandleExit(in PhxEventModel evModel) {
        var one = evModel.one;
        var two = evModel.two;
        if (one.entityType == EntityType.Monster || one.entityType == EntityType.Bullet) {

            return;
        }

        if (one.entityType == EntityType.Bullet || two.entityType == EntityType.Monster) {

            return;
        }

        if (one.entityType == EntityType.Monster || two.entityType == EntityType.WeaponForm) {

            return;
        }

        if (one.entityType == EntityType.WeaponForm || two.entityType == EntityType.Monster) {

            return;
        }
    }

    public void HandleTriggerEnter(EntityIDArgs one, EntityIDArgs two, Vector2 normal, int layerMask_one, int layerMask_two) {
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.TryAdd(one, two, normal, layerMask_one, layerMask_two);
    }

    public void HandleTriggerExit(EntityIDArgs one, EntityIDArgs two, Vector2 normal, int layerMask_one, int layerMask_two) {
        var phxEventRepo = mainContext.rootRepo.phxEventRepo;
        phxEventRepo.TryAdd(one, two, normal, layerMask_one, layerMask_two);
    }

}