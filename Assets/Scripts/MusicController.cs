using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public AudioSource islandMusic, caveMusic;
    public bool inCave;

    void Update() {
        float islandVolumeTarget = inCave ? 0f : 1f;
        float caveVolumeTarget = inCave ? 1f : 0f;

        islandMusic.volume += (islandVolumeTarget - islandMusic.volume) * 0.05f;
        caveMusic.volume += (caveVolumeTarget - caveMusic.volume) * 0.05f;
    }
}
