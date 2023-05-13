using System;
using UnityEngine;

public class Panel_Login : MonoBehaviour {

    Action OnAnyKeyHandle;

    public void Init(Action action) {
        OnAnyKeyHandle = action;
    }

    public void Tick(float dt) {
        if (Input.anyKey) {
            OnAnyKeyHandle.Invoke();
        }
    }

}