using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    private GameObject player;
    private SpriteRenderer sr;
    public float playerSpotDistance = 5f, ratSpotDistance = 2f;
    public float speed = 3f;
    private GameObject rat;
    public bool followingRat;
    private CircleCollider2D c2d;

    private void Start() {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<CircleCollider2D>();
    }
    
    private void Update() {
        if(rat == null) {
            Rat r = FindObjectOfType<Rat>();
            if (r != null) rat = r.gameObject;
        }

        c2d.isTrigger = followingRat;

        if (!followingRat) {
            if (Vector2.Distance(player.transform.position, transform.position) <= playerSpotDistance) {
                sr.flipX = player.transform.position.x > transform.position.x;
            } else {
                sr.flipX = false;
            }
        } else {
            if (rat != null) {
                sr.flipX = rat.transform.position.x > transform.position.x;
                transform.position = Vector2.MoveTowards(transform.position, rat.transform.position, speed * Time.deltaTime);
            } else {
                followingRat = false;
            }
        }
    }
}
