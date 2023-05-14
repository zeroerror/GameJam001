using UnityEngine;

public class WeaponFSMStateModel_Shooting {

    bool isEntering;
    public bool IsEntering => isEntering;
    public void SetIsEntering(bool value) => isEntering = value;

    bool isExiting;
    public bool IsExiting => isExiting;
    public void SetIsExiting(bool value) => isExiting = value;

    Vector2 shootTargetPos;
    public Vector2 ShootTargetPos => shootTargetPos;
    public void SetShootTargetPos(Vector2 value) => shootTargetPos = value;

    public float time;

    // 三连发 
    public int triplet_count;
    public float triplet_time;

    public WeaponFSMStateModel_Shooting() { }

    public void Reset() {
        isEntering = false;
        isExiting = false;
        time = 0f;
        triplet_count = 0;
        triplet_time = 0f;
    }

}