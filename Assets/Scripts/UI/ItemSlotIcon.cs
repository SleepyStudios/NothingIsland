using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    private Image icon;

    private void Awake() {
        icon = GetComponent<Image>();
        icon.enabled = false;
    }

    public void SetItem(Item item) {
        this.item = item;

        if (item != null) {
            icon.enabled = true;
            icon.sprite = item.sprite;
        } else {
            icon.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (item == null) return;
        FindObjectOfType<Tooltip>().InitTooltip(transform.position, item.name);
    }

    public void OnPointerExit(PointerEventData eventData) {
        FindObjectOfType<Tooltip>().Reset();
    }
}
