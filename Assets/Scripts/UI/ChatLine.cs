using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.LWRP;

public class ChatLine : MonoBehaviour {
    private float tmrFadeout;
    private bool fade;
    private TextMeshProUGUI textMesh;
    private Color fadedColour;
    private GameObject player;

    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
        fadedColour = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
        player = GameObject.Find("Player");
    }

    private void Update() {
        bool torchEnabled = player.GetComponent<Light2D>().enabled;
        textMesh.color = new Color(torchEnabled ? 1 : 0, torchEnabled ? 1 : 0, torchEnabled ? 1 : 0, textMesh.color.a);

        if(!fade) {
            tmrFadeout += Time.deltaTime;
            if (tmrFadeout >= 5) {
                fade = true;
                tmrFadeout = 0;
            }
        } else {
            textMesh.color = Color.Lerp(textMesh.color, fadedColour, 0.05f);
        }
    }

    public void SetText(string text) {
        textMesh.text = text;
    }
}
