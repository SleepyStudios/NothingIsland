using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private GameObject player;
    private Animator animator;

    private void Start() {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    public void Popup() {
        animator.SetTrigger("triggerInv");
    }

    public ItemSlot FindFreeSlot() {
        foreach(ItemSlot slot in GetComponentsInChildren<ItemSlot>()) {
            if (!slot.HasItem()) return slot;
        }
        return null;
    }

    public void PickupItem(Item item) {
        ItemSlot freeSlot = FindFreeSlot();
        if (freeSlot == null) {
            GameObject worldItem = Instantiate(Resources.Load("Interactables/WorldItem") as GameObject, player.transform.position, Quaternion.identity);
            worldItem.GetComponent<WorldItem>().SetItem(item);
        } else {
            freeSlot.PickupItem(Instantiate(item));
            Popup();
        }
    }

    public bool PickupWorldItem(Item item) {
        ItemSlot freeSlot = FindFreeSlot();
        if (freeSlot == null) {
            FindObjectOfType<Chatbox>().AddText("I don't have any space for that");
            return false;
        } else {
            freeSlot.PickupItem(Instantiate(item));
            Popup();
        }
        return true;
    }

    public bool HasItem(string itemName) {
        foreach (ItemSlot slot in GetComponentsInChildren<ItemSlot>()) {
            if (slot.HasItem() && slot.GetItem().name == itemName) return true;
        }
        return false;
    }

    public void FindAndDestroyItem(string itemName) {
        foreach (ItemSlot slot in GetComponentsInChildren<ItemSlot>()) {
            if (slot.HasItem() && slot.GetItem().name == itemName) {
                slot.DestroyItem();
                return;
            }
        }
    }

    protected void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc) {
        ItemSlot source = desc.sourceCell.GetComponentInParent<ItemSlot>();

        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd) {
            if (desc.permission) {
                FindObjectOfType<InteractMenu>().Cancel();
            }
        }
    }
}
