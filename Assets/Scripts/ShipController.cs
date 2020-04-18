using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    public float speed = 10f, positionErrorMargin = 0.1f;
    public Waypoint[] waypoints;
    private int nextPositionIndex;
    private float tmrWait;
    private bool reverse;
    private SpriteRenderer sr;
    private Animator animator;
    private GameObject player;

    private void Start() {
        transform.position = waypoints[0].pos;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    private void Update() {
        if (!PersistenceManager.instance.inGame) return;

        if (Vector2.Distance(transform.position, waypoints[nextPositionIndex].pos) <= positionErrorMargin) {
            tmrWait += Time.deltaTime;
            float random = 0f;
            if (waypoints[nextPositionIndex].waitTime > 0) {
                random = Random.Range(-1.5f, 2.0f);
            }
            if (tmrWait >= waypoints[nextPositionIndex].waitTime + random) {
                if (nextPositionIndex + 1 == waypoints.Length) {
                    reverse = true;
                    sr.flipX = true;
                } else if (nextPositionIndex - 1 == -1) {
                    reverse = false;
                    sr.flipX = false;
                }

                nextPositionIndex += reverse ? -1 : 1;
                Vector2 dir = waypoints[nextPositionIndex].pos - new Vector2(transform.position.x, transform.position.y);
                tmrWait = 0;
            }
        } else {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[nextPositionIndex].pos, speed * Time.deltaTime);
        }
    }

    public void OnWin() {
        nextPositionIndex = 0;
        waypoints = new Waypoint[] { new Waypoint() { pos = new Vector2(player.transform.position.x, player.transform.position.y) } };
        sr.flipX = false;
    }
}
