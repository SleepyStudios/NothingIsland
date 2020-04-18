using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rat : MonoBehaviour {
    public PolygonCollider2D area;
    private Vector3 nextPos;
    private SpriteRenderer sr;
    public float speed = 8f;

    private void Start() {
        GameObject[] areas = GameObject.FindGameObjectsWithTag("RatMovementArea");

        area = areas.OrderBy(a => Vector2.Distance(transform.position, a.transform.position)).ToArray()[0].GetComponent<PolygonCollider2D>();

        sr = GetComponent<SpriteRenderer>();
        SetNextPos();

        Snake snake = GameObject.Find("Snake").GetComponent<Snake>();
        float distance = Vector2.Distance(transform.position, snake.transform.position);
        if (distance <= snake.ratSpotDistance) {
            snake.followingRat = true;
        }
    }

    private void Update() {
        if (!PersistenceManager.instance.inGame) return;

        if(Vector2.Distance(transform.position, nextPos) <= 1f) {
            SetNextPos();
        } else {
            transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    private void SetNextPos() {
        Vector3 randomPoint = GenerateRandomPointInBounds();
        while(!area.OverlapPoint(randomPoint)) {
            randomPoint = GenerateRandomPointInBounds();
        }
        nextPos = randomPoint;

        Vector3 dir = nextPos - transform.position;
        sr.flipX = dir.x < 0f;
    }

    private Vector3 GenerateRandomPointInBounds() {
        return new Vector3(Random.Range(area.bounds.min.x, area.bounds.max.x), Random.Range(area.bounds.min.y, area.bounds.max.y));
    }
}
