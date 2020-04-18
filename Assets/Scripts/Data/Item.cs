using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RecipeResult {
    public Item item;
    public int num;
}

[System.Serializable]
public struct Recipe {
    public Item other;
    public RecipeResult[] results;
}

[System.Serializable]
public struct Action {
    public string text;
    public string methodToCall;

    public Action(string text, string methodToCall) {
        this.text = text;
        this.methodToCall = methodToCall;
    }
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {
    public new string name;
    public string description;
    public Sprite sprite;
    public Recipe[] recipes;
    public Action[] actions = {
        new Action("Examine", "ExamineItem"),
        new Action("Drop", "DropItem")
    };
    public CustomDataDict customData;
}
