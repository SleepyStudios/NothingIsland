using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {
    private Animator animator;
    private GameObject player;
    private Vector2 nextPos;
    private GameObject nextLandMass;

    private void Start() {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public void Init(Vector2 pos, GameObject landMass) {
        nextPos = pos;
        nextLandMass = landMass;
        animator.enabled = true;
    }

    public void Teleport() {
        player.transform.position = nextPos;
        Camera.main.GetComponent<FollowPlayer>().Init(nextLandMass);
        Camera.main.GetComponent<FollowPlayer>().Snap();
    }

    public void OnAnimationComplete() {
        animator.enabled = false;
    }
}
