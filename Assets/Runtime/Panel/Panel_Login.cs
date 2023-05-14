using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Login : MonoBehaviour {

    Action OnAnyKeyHandle;
    [SerializeField] Image img_notice;

    public void Init(Action action) {
        OnAnyKeyHandle = action;
    }

    public void Tick(float dt) {
        if (Input.anyKey) {
            OnAnyKeyHandle.Invoke();
        }

        var color = img_notice.color;
        float a = Mathf.PingPong(Time.time, 0.5f);
        color.a = a;
        img_notice.color = color;

    }

}