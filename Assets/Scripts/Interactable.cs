using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct Interaction {
    public Item requirement;
    public string methodToCall;
    public bool requiresNotHungry;
}

public class Interactable : MonoBehaviour {
    public CustomDataDict customData;
    public Interaction[] interactions;
    public string noInteractionsText;
    public float interactDistance = 1.5f;
    private Texture2D cursor, interactionCursor;
    protected Inventory inv;
    private GameObject player;
    private bool moveBoulder;
    private Vector3 boulderPosToGoTo;
    private int starfishCount, treeShakeCount;
    public AudioClip audioClip;

    private void Start() {
        cursor = Resources.Load("Cursors/cursor") as Texture2D;
        interactionCursor = Resources.Load("Cursors/interaction_cursor") as Texture2D;
        inv = FindObjectOfType<Inventory>();
        player = GameObject.Find("Player");
    }

    private void FixedUpdate() {
        if (moveBoulder) {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, boulderPosToGoTo, 0.33f * Time.deltaTime);
        }
    }

    private void OnMouseEnter() {
        Cursor.SetCursor(interactionCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit() {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnDestroy() {
        OnMouseExit();
    }

    private void OnMouseDown() {
        if(Input.GetMouseButtonDown(0)) {
            if (Vector2.Distance(transform.position, player.transform.position) <= interactDistance) {
                    bool didInteract = false;

                    foreach (Interaction i in interactions) {
                        if (i.requirement == null || inv.HasItem(i.requirement.name)) {
                            if (i.requiresNotHungry && player.GetComponent<PlayerController>().hungry) {
                                FindObjectOfType<Chatbox>().AddText(noInteractionsText);
                                return;
                            }

                            didInteract = true;
                            Invoke(i.methodToCall, 0f);
                        }
                    }

                    if (!didInteract && noInteractionsText.Length > 0) {
                        FindObjectOfType<Chatbox>().AddText(noInteractionsText);
                    }

                    if (didInteract && audioClip != null) {
                        PersistenceManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip);
                    }
            } else {
                FindObjectOfType<Chatbox>().AddText("I'm too far away from that");
            }
        }
    }

    private void PickupBottle() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Bottle") as Item));
        inv.PickupItem(Instantiate(Resources.Load("Items/Note") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void PickupRock() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Rock") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void CutGrass() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Grass") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["cutText"]);
        Destroy(gameObject);
    }

    private void PickupStarfish() {
        starfishCount++;
        if (starfishCount == 1) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        } else if (starfishCount == 2) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText2"]);
        } else if (starfishCount == 3) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText3"]);
        } else if (starfishCount > 3) {
            inv.PickupItem(Instantiate(Resources.Load("Items/Starfish") as Item));
            FindObjectOfType<Chatbox>().AddText(customData["pickupText4"]);
            Destroy(gameObject);
        }
    }

    private void PushBoulder() {
        FindObjectOfType<Chatbox>().AddText(customData["pushText"]);
        if (!moveBoulder) {
            boulderPosToGoTo = gameObject.transform.position + new Vector3(1, 0, 0);
            moveBoulder = true;
        }
    }

    private void PickupMap() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Map") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void PickupShell() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Seashell") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void PickupBranch() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Branch") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        FindObjectOfType<Chatbox>().AddText(customData["pickupText2"]);
        Destroy(gameObject);
    }

    private void PickupRat() {
        if(inv.PickupWorldItem(Instantiate(Resources.Load("Items/Rat") as Item))) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
            Destroy(gameObject);
        } else {
            FindObjectOfType<Chatbox>().AddText(customData["noSpaceText"]);
        }
    }

    private void PickupShovel() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Shovel") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void RotateTotemPart() {
        FindObjectOfType<Chatbox>().AddText(customData["rotateText"]);
        GetComponent<TotemController>().NextFace();
    }

    private void DigTreasure() {
        inv.PickupItem(Instantiate(Resources.Load("Items/Chest") as Item));
        FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        Destroy(gameObject);
    }

    private void PlaceExplosives() {
        FindObjectOfType<Chatbox>().AddText(customData["explodeText"]);
        FindObjectOfType<Inventory>().FindAndDestroyItem("Explosives");

        Instantiate(Resources.Load("Interactables/Explosives") as GameObject, transform.position, Quaternion.identity);
        Instantiate(Resources.Load("Interactables/Explosions") as GameObject, transform.position, Quaternion.identity);
    }

    private void ReadSign() {
        string note = "Notes/" + customData["note"];
        FindObjectOfType<NoteController>().OpenNote(Instantiate(Resources.Load(note)) as Note);
    }

    private void ShakeTree() {
        treeShakeCount++;
        if (treeShakeCount == 1) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText"]);
        } else if (treeShakeCount == 2) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText2"]);
        } else if (treeShakeCount == 3) {
            FindObjectOfType<Chatbox>().AddText(customData["pickupText3"]);
        } else if (treeShakeCount == 4) {
            Vector2 position = new Vector2(transform.position.x + 0.5f, transform.position.y - 3f);
            Instantiate(Resources.Load("Interactables/Coconut") as GameObject, position, Quaternion.identity);

            FindObjectOfType<Chatbox>().AddText(customData["pickupText4"]);
        } else {
            FindObjectOfType<Chatbox>().AddText(noInteractionsText);
        }
    }

    private void PickupCoconut() {
        inv.PickupItem(Resources.Load("Items/Coconut") as Item);
        Destroy(gameObject);
    }
}