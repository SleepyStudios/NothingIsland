using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {
    private InteractMenu interactMenu;
    private GameObject player;

    private void Start() {
        interactMenu = GameObject.Find("InteractMenu").GetComponent<InteractMenu>();
        player = GameObject.Find("Player");
    }

    public bool HasItem() {
        return transform.childCount > 0;
    }

    public Item GetItem() {
        if (!HasItem()) return null;
        return transform.GetChild(0).GetComponent<ItemSlotIcon>().item;
    }

    public void PickupItem(Item item) {
        GameObject itemSlotIcon = Instantiate(Resources.Load("UI/ItemSlotIcon") as GameObject, transform.position, Quaternion.identity, transform);
        itemSlotIcon.GetComponent<ItemSlotIcon>().SetItem(item);
    }

    public void DestroyItem() {
        DestroyImmediate(transform.GetChild(0).gameObject);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!HasItem()) return;

        if (eventData.button == PointerEventData.InputButton.Right) {
            Button[] buttons = interactMenu.InitMenu(transform.position, GetItem().actions);
            for(int i = 0; i < buttons.Length; i++) {
                int counter = i;
                string methodToCall = GetItem().actions[counter].methodToCall;

                buttons[i].onClick.AddListener(delegate { InvokeOption(methodToCall); });
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = GetItem().actions[i].text;
            }
        }
    }

    public void InvokeOption(string methodToCall) {
        Invoke(methodToCall, 0f);
    }

    private void ExamineItem() {
        if (!HasItem()) return;

        FindObjectOfType<Chatbox>().AddText(GetItem().description);
    }

    private void DropItem() {
        if (!HasItem()) return;

        if (GetItem().name == "Torch") player.GetComponent<PlayerController>().ToggleTorch(false);

        GameObject worldItem = Instantiate(Resources.Load("Interactables/WorldItem") as GameObject, player.transform.position - new Vector3(0, 0.75f, 0), Quaternion.identity);
        worldItem.GetComponent<WorldItem>().SetItem(GetItem());
        DestroyItem();
    }

    private void EatItem() {
        if (!HasItem()) return;

        player.GetComponent<PlayerController>().ToggleHungry();
        FindObjectOfType<Chatbox>().AddText(GetItem().customData["eatText"]);
        DestroyItem();
    }

    private void ReadNote() {
        if (!HasItem()) return;

        string note = "Notes/" + GetItem().customData["note"];
        FindObjectOfType<NoteController>().OpenNote(Instantiate(Resources.Load(note)) as Note);
    }

    private void DropRat() {
        if (!HasItem()) return;

        GameObject rat = Instantiate(Resources.Load("Interactables/Rat") as GameObject, player.transform.position, Quaternion.identity);
        rat.GetComponent<Rat>().area = GameObject.Find("Rat Movement Area").GetComponent<PolygonCollider2D>();
        DestroyItem();
    }

    private void DropGrass() {
        if (!HasItem()) return;

        GameObject worldItem = Instantiate(Resources.Load("Interactables/WorldItem") as GameObject, player.transform.position - new Vector3(0, 0.75f, 0), Quaternion.identity);
        worldItem.GetComponent<WorldItem>().SetItem(GetItem());
        FindObjectOfType<Chatbox>().AddText(GetItem().customData["dropText"]);
        DestroyItem();
    }
}
