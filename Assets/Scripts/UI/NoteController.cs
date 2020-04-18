using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NoteController : MonoBehaviour {
    private Image[] images;
    private GameObject noteButton;
    private Vector2 originalPos;

    private void Start() {
        images = GetComponentsInChildren<Image>();
        images.All(i => { i.enabled = false; return true; } );
        noteButton = GameObject.Find("Note Button");
        originalPos = noteButton.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OpenNote(Note note) {
        if(note.isSign) {
            noteButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(220, -100);
        }

        images[1].sprite = note.sprite;
        images.All(i => { i.enabled = true; return true; });
    }

    public void CloseNote() {
        noteButton.GetComponent<RectTransform>().anchoredPosition = originalPos;
        images.All(i => { i.enabled = false; return true; });
    }
}
