using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float shakeDuration;
    public float shakeIntensity = 0.2f;
    public float decreaseFactor = 0.4f;
    Vector3 originalPos;

    private void OnEnable() {
        originalPos = transform.localPosition;
    }

    void Update() {
        if (shakeDuration > 0) {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeIntensity;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void Shake(float intensity, float duration) {
        shakeIntensity = intensity;
        shakeDuration = duration;
    }
}
