using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {
    private TotemController baseBit, midBit, topBit;
    private bool shook;
    private GameObject islandShoreWater;
    private Color fadedOut;
    public AudioClip audioClip;

    private void Start() {
        baseBit = transform.GetChild(0).GetComponent<TotemController>();
        midBit = transform.GetChild(1).GetComponent<TotemController>();
        topBit = transform.GetChild(2).GetComponent<TotemController>();
        islandShoreWater = GameObject.Find("island_shore_water");
        fadedOut = new Color(1, 1, 1, 0);
    }

    private void Update() {
        if(!shook) {
            if (baseBit.index == 2 && midBit.index == 3 && topBit.index == 0) {
                shook = true;
                FindObjectOfType<CameraShake>().Shake(0.15f, 1.25f);
                FindObjectOfType<Chatbox>().AddText("Woah!");
                GameObject.Find("Island").GetComponent<EdgeCollider2D>().enabled = false;
                PersistenceManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip);
            }
        }

        if(shook) {
            islandShoreWater.GetComponent<SpriteRenderer>().color = Color.Lerp(islandShoreWater.GetComponent<SpriteRenderer>().color, fadedOut, 0.01f);
        }
    }
}
