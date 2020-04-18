using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatbox : MonoBehaviour {
    public GameObject content;

    public void AddText(string text) {
        GameObject chatLine = Instantiate(Resources.Load("UI/ChatLine") as GameObject, Vector2.zero, Quaternion.identity, content.transform);
        chatLine.GetComponent<ChatLine>().SetText(text);

        int maxLines = 4;
        if(content.transform.childCount > maxLines) {
            for(int i = 0; i < content.transform.childCount - maxLines; i++) {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
}
