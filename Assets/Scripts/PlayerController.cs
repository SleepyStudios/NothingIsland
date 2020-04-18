using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerController : MonoBehaviour {
    public float speed = 5f, slowedSpeed = 2f, runningAwaySpeed = 7f;
    private Rigidbody2D rb;
    private Inventory inv;
    private SpriteRenderer sr;
    private Animator animator;
    public bool hungry = true;
    private Light2D torch;
    private float targetCameraSize;
    private MusicController mc;

    private bool slowDown;
    private bool runningAway;
    private Vector3 runAwayPos;
    private float xUnitSpeed, yUnitSpeed;
    private float xSpeed, ySpeed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        torch = GetComponent<Light2D>();
        torch.enabled = false;
        targetCameraSize = Camera.main.orthographicSize;
        mc = FindObjectOfType<MusicController>();
    }

    private void FixedUpdate() {
        if (!PersistenceManager.instance.inGame) return;

        // Camera
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetCameraSize, 0.01f);

        // Speed
        float trueSpeed = speed;
        if (runningAway) trueSpeed = runningAwaySpeed;
        if (slowDown) trueSpeed = slowedSpeed;

        if(!runningAway) {
            // Normal movement
            xUnitSpeed = Input.GetAxis("Horizontal");
            yUnitSpeed = Input.GetAxis("Vertical");
        } else {
            // Controlled movement
            Vector3 dir = runAwayPos - transform.position;
            xUnitSpeed = dir.x;
            yUnitSpeed = dir.y;

            if (Vector3.Distance(transform.position, runAwayPos) <= 1f) runningAway = false;
        }

        xSpeed = xUnitSpeed * trueSpeed * Time.fixedDeltaTime;
        ySpeed = yUnitSpeed * trueSpeed * Time.fixedDeltaTime;
        if (xUnitSpeed != 0) {
            sr.flipX = xUnitSpeed > 0;
        }

        // Magnitude to make diagonal movements the same speed as vertical and horizontal
        float magnitude = 1.0f;
        if (!(xUnitSpeed == 0 && yUnitSpeed == 0)) {
            magnitude = Mathf.Sqrt(Mathf.Pow(xUnitSpeed, 2) + Mathf.Pow(yUnitSpeed, 2));
        } else {
            magnitude = 1.0f;
        }
  
        animator.SetBool("walking", xSpeed != 0 || ySpeed != 0);
        rb.MovePosition(rb.position + new Vector2(xSpeed / magnitude, ySpeed / magnitude));
    }

    private void LateUpdate() {
        sr.sortingOrder = (int)(Camera.main.WorldToScreenPoint(sr.bounds.min).y + 50) * -1;
    }

    public void ToggleHungry() {
        hungry = !hungry;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Teleport")) {
            Teleporter t = collision.GetComponent<Teleporter>();
            if(t.requiresTorch && !inv.HasItem("Torch")) {
                FindObjectOfType<Chatbox>().AddText(t.cantUseMessage);
                return;
            }
            
            ToggleTorch(t.requiresTorch);
            mc.inCave = t.requiresTorch;

            FindObjectOfType<TransitionController>().Init(t.pos, t.landMass);
        }

        if(collision.CompareTag("MountainTop")) {
            targetCameraSize = 15;
        }

        if (collision.CompareTag("MountainPath")) {
            slowDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("MountainTop")) {
            targetCameraSize = 5;
        }

        if (collision.CompareTag("MountainPath")) {
            slowDown = false;
        }
    }

    public void SetRunningAway(Vector2 runAwayPos) {
        this.runAwayPos = new Vector3(runAwayPos.x, runAwayPos.y, 0);
        runningAway = true;
    }

    public void ToggleTorch(bool on) {
        torch.enabled = on;
    }
}
