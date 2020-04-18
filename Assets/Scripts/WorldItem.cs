using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : Interactable {
    public Item item;
    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item) {
        if (item == null) return;
        this.item = item;
        sr.sprite = item.sprite;

        if(item.name == "Grass") gameObject.AddComponent<Grass>();
    }

    private void PickupItem() {
        if(inv.PickupWorldItem(item)) {
            if (item.name == "Torch") FindObjectOfType<PlayerController>().ToggleTorch(true);
            Destroy(gameObject);
        }
    }
}
