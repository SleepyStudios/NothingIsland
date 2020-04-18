using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform player;
    private float leftBound, rightBound, bottomBound, topBound;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    private void Start () {
        player = GameObject.Find("Player").transform;
        Init(GameObject.Find("Island"));
    }

    public void Init(GameObject landMass) {
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        SpriteRenderer background = landMass.GetComponent<SpriteRenderer>();
        leftBound = background.bounds.min.x + horzExtent;
        rightBound = background.bounds.max.x - horzExtent;
        bottomBound = background.bounds.min.y + vertExtent;
        topBound = background.bounds.max.y - vertExtent;
    }

    private void FixedUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, GetDestination(), ref velocity, dampTime);
    }

    private Vector3 GetDestination() {
        Vector3 point = Camera.main.WorldToViewportPoint(player.position);
        Vector3 delta = player.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        destination.x = Mathf.Clamp(destination.x, leftBound, rightBound);
        destination.y = Mathf.Clamp(destination.y, bottomBound, topBound);
        return destination;
    }

    public void Snap() {
        transform.position = GetDestination();
    }
}
