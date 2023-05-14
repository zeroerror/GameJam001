using UnityEngine;
using GameArki.FPEasing;

public class CameraManager : MonoBehaviour {

    Camera cam => Camera.main;

    Vector3 originPos = new Vector3(0, 11.25f, -10);

    EasingType easingType;
    float duration;
    float time;
    Vector2 targetOffset;

    public void Ctor() {
        cam.transform.position = originPos;
    }

    public void Tick(float dt) {
        if (time >= duration) {
            return;
        }
        float v = WaveHelper.SinWaveReductionEasing(easingType, time, duration, 1f, 40f, 0);
        v = v * (time / duration);
        Vector2 pos = Vector2.Lerp(-targetOffset, targetOffset, v);
        cam.transform.position = originPos + (Vector3)pos;
        time += dt;
        if (time >= duration) {
            cam.transform.position = originPos;
        }
    }

    public void Shake_Normal() {
        Shake(EasingType.OutQuad, new Vector2(0.1f, 0.1f), 0.1f);
    }

    public void Shake_Rocket_Shoot() {
        Shake(EasingType.OutQuad, new Vector2(0.3f, 0.3f), 0.1f);
    }

    public void Shake_Rocket_Hit() {
        Shake(EasingType.OutQuad, new Vector2(0.8f, 0.8f), 0.1f);
    }

    public void Shake(EasingType easingType, Vector2 offset, float duration) {
        this.easingType = easingType;
        this.duration = duration;
        this.time = 0;
        this.targetOffset = offset;
    }

}