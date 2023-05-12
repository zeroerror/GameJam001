using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleEntity {

    EntityIDComponent idCom;
    public EntityIDComponent IDCom => idCom;

    RoleInputComponent inputCom;
    public RoleInputComponent InputCom => inputCom;

    RoleFSMComponent fsmCom;
    public RoleFSMComponent FSMCom => fsmCom;

    GameObject go;
    public GameObject GO => go;

    public RoleEntity() {
        idCom = new EntityIDComponent();
        idCom.SetEntityType(EntityType.Role);

        inputCom = new RoleInputComponent();
        fsmCom = new RoleFSMComponent();
    }

    public void Inject(GameObject go) {
        this.go = go;
    }

}
