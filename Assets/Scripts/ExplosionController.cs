using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExplosionController : MonoBehaviour {
    public AudioClip audioClip;
    private int explosions;
    public Vector2 playerRunAwaySpot;

    private void Start() {
        transform.parent.GetComponentsInChildren<SpriteRenderer>().All(sr => { sr.enabled = false; return true; });

        Invoke("RunAway", 1f);
        Invoke("StartExplosives", 2f);
    }

    private void StartExplosives() {
        transform.parent.GetComponentsInChildren<SpriteRenderer>().All(sr => { sr.enabled = true; return true; });
        transform.parent.GetComponentsInChildren<Animator>().All(a => { a.enabled = true; return true; });
        Destroy(GameObject.Find("Explosives(Clone)"));
    }

    private void RunAway() {
        FindObjectOfType<PlayerController>().SetRunningAway(playerRunAwaySpot);
    }

    public void OnAnimStart() {
        if (explosions < 3) PersistenceManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip);
        if (explosions == 1) EnableWin();
        if (explosions == 3) {
            transform.parent.GetComponentsInChildren<SpriteRenderer>().All(sr => { sr.enabled = false; return true; });
            transform.parent.GetComponentsInChildren<Animator>().All(a => { a.enabled = false; return true; });
        }
    }

    public void OnAnimComplete() {
        explosions++;
    }

    public void EnableWin() {
        FindObjectOfType<ShipController>().OnWin();
        GameObject.Find("WinState").GetComponent<Animator>().enabled = true;
    }
}
