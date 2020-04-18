using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour {
    public Vector3 posToGoTo;
    public bool shouldMove;
    public float speed;

    public void StartMoving() {
        shouldMove = true;
    }

    private void Update() {
        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x, 0.25f * Mathf.Sin(Time.time), currentPos.z);

        if (shouldMove) {
            transform.position = Vector2.MoveTowards(transform.position, posToGoTo, speed * Time.deltaTime);
        }
    }
}