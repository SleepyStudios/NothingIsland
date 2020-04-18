using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VennSegment : MonoBehaviour {
    private GameObject worldItem;
    private bool destroyItem;
    private Color fadedColour;
    public Item[] possibleItems;

    private void Start() {
        fadedColour = new Color(0f, 0f, 0f, 0f);
    }

    private void Update() {
        if (destroyItem && worldItem != null) {
            SpriteRenderer sr = worldItem.GetComponent<SpriteRenderer>();
            sr.color = Color.Lerp(sr.color, fadedColour, 0.05f);
            if(Mathf.Approximately(sr.color.a, 0.1f)) {
                Destroy(worldItem);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (destroyItem) return;
        if(collision.CompareTag("WorldItem")) {
            worldItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (destroyItem) return;
        if (collision.CompareTag("WorldItem")) {
            worldItem = null;
        }
    }

    public WorldItem GetWorldItem() {
        return worldItem.GetComponent<WorldItem>();
    }

    public bool HasCorrectItem() {
        if (destroyItem) return false;
        if (worldItem == null) return false;

        Item currentItem = worldItem.GetComponent<WorldItem>().item;

        foreach(Item option in possibleItems) {
            if (currentItem.name == option.name) return true;
        }

        return false;
    }

    public void DestroyItem() {
        destroyItem = true;
        Destroy(worldItem.GetComponent<WorldItem>());
    }
}
