using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour {
    public Sprite[] sprites;
    public int index;
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void NextFace() {
        index = (index + 1) % sprites.Length;
        sr.sprite = sprites[index];
    }
}
