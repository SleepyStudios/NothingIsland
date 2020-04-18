using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    private void Start() {
        Reset();
    }

    public void InitTooltip(Vector2 pos, string itemName) {
        transform.position = new Vector2(pos.x + GetComponent<RectTransform>().sizeDelta.x / 2, pos.y + GetComponent<RectTransform>().sizeDelta.y / 1.5f);
        GetComponentInChildren<TextMeshProUGUI>().text = itemName;
    }

    public void Reset() {
        transform.position = new Vector2(-999, -999);
    }
}
