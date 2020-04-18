using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Torch : MonoBehaviour {
    private Light2D l2d;
    private float targetOuterRadius;
    public float minOuterRadius, maxOuterRadius;
    public float timeBetweenFlickers;
    private float tmrFlicker;

    private void Start() {
        l2d = GetComponent<Light2D>();
        targetOuterRadius = l2d.pointLightOuterRadius;
    }

    private void Update() {
        if (!l2d.enabled) return;

        tmrFlicker += Time.deltaTime;
        if(tmrFlicker >= timeBetweenFlickers) {
            targetOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
            tmrFlicker = 0;
        }

        l2d.pointLightOuterRadius = Mathf.Lerp(l2d.pointLightOuterRadius, targetOuterRadius, 0.05f);
    }
}
