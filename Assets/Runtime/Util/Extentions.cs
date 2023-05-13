using UnityEngine;

public static class Extentions {

    public static void Hide(this MonoBehaviour mono) {
        mono.gameObject.SetActive(false);
    }

    public static void Show(this MonoBehaviour mono) {
        mono.gameObject.SetActive(true);
    }
}