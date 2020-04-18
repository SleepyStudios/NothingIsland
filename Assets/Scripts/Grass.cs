using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {
    private float tmrDry;

    private void Update() {
        tmrDry += Time.deltaTime;
        if(tmrDry >= 20) {
            SpawnDryGrass();
            tmrDry = 0;
        }
    }

    private void SpawnDryGrass() {
        Item dryGrass = Instantiate(Resources.Load("Items/DryGrass") as Item);
        GameObject worldItem = Instantiate(Resources.Load("Interactables/WorldItem") as GameObject, transform.position, Quaternion.identity);
        worldItem.GetComponent<WorldItem>().SetItem(dryGrass);
        Destroy(gameObject);
    }
}
