public class RoleDomain {

    MainContext mainContext;
    Factory factory;

    public RoleDomain(MainContext mainContext, Factory factory) {
        this.mainContext = mainContext;
    }

    public void BackInput() {
        var roleRepo = mainContext.rootRepo.roleRepo;
        var inputGetter = mainContext.freeInputCore.Getter;
        roleRepo.ForeachAll((role) => {
            var inputCom = role.InputCom;

            int moveHorDir = 0;
            if (inputGetter.GetPressing(InputKeyCollection.MOVE_LEFT)) {
                moveHorDir -= 1;
            } else if (inputGetter.GetPressing(InputKeyCollection.MOVE_RIGHT)) {
                moveHorDir += 1;
            }
            if (moveHorDir != 0) {
                inputCom.SetMoveHorDir(moveHorDir);
            }

            if (inputGetter.GetDown(InputKeyCollection.JUMP)) {
                inputCom.SetInputJump(true);
            }

            if (inputGetter.GetDown(InputKeyCollection.PICK)) {
                inputCom.SetInputPick(true);
            }

            if (inputGetter.GetPressing(InputKeyCollection.SHOOT)) {
                inputCom.SetInputShoot(true);
            }

        });
    }

    public bool TryCreateRole(int typeID, out RoleEntity role) {
        if (!factory.TryCreateRole(typeID, out role)) {
            return false;
        }

        var idService = mainContext.rootService.idService;
        var id = idService.PickRoleID();
        var idCom = role.IDCom;
        idCom.SetEntityID(id);

        var roleRepo = mainContext.rootRepo.roleRepo;
        roleRepo.TryAdd(typeID, role);

        return false;
    }

}