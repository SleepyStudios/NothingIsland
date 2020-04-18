using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractMenu : MonoBehaviour {
    float offset;

    private void Start() {
        transform.position = new Vector2(-999, -999);
        offset = GetComponent<RectTransform>().sizeDelta.y;
    }

    public Button[] InitMenu(Vector2 pos, Action[] actions) {
        for(int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        transform.position = new Vector2(pos.x, pos.y + (offset * (actions.Length + 1)));

        List<Button> buttons = new List<Button>();
        for(int i = 0; i < actions.Length; i++) {
            Button b = Instantiate(Resources.Load("UI/InteractMenuButton") as GameObject, new Vector2(transform.position.x, transform.position.y - offset - (i * offset)), Quaternion.identity, transform)
                .GetComponent<Button>();
            b.onClick.AddListener(Cancel);
            buttons.Add(b);
        }

        GameObject cancel = Instantiate(Resources.Load("UI/InteractMenuButton") as GameObject, new Vector2(transform.position.x, transform.position.y - offset - (buttons.Count * offset)), Quaternion.identity, transform);
        cancel.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
        cancel.GetComponent<Button>().onClick.AddListener(Cancel);

        return buttons.ToArray();
    }

    public void Cancel() {
        transform.position = new Vector2(-999, -999);

        for (int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void Update() {
        HideIfClickedOutside();
    }

    private void HideIfClickedOutside() {
        if (transform.childCount > 1 && Input.GetMouseButtonUp(0)) {
            foreach (RectTransform child in transform.GetComponentsInChildren<RectTransform>()) {
                if (RectTransformUtility.RectangleContainsScreenPoint(child, Input.mousePosition, Camera.main)) return;
            }

            Cancel();
        }
    }
}
