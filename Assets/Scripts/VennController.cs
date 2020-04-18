using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VennController : MonoBehaviour {
    private VennSegment[] vennSegments;
    private bool solved;
    public bool leftCorrect, middleCorrect, rightCorrect;

    private void Start() {
        vennSegments = GetComponentsInChildren<VennSegment>();
    }

    private void Update() {
        if (solved) return;

        int correctItems = vennSegments.Where(v => v.HasCorrectItem()).ToArray().Length;

        if(correctItems == 3) {
            solved = true;

            Vector2 position = vennSegments[1].GetWorldItem().transform.position;

            foreach(VennSegment vs in vennSegments) {
                vs.DestroyItem();
            }

            FindObjectOfType<CameraShake>().Shake(0.1f, 0.25f);

            GameObject worldItem = Instantiate(Resources.Load("Interactables/WorldItem") as GameObject, position, Quaternion.identity);
            worldItem.GetComponent<WorldItem>().SetItem(Resources.Load("Items/TotemClue") as Item);
        }
    }
}
