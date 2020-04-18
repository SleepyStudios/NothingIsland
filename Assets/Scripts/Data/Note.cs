using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Note")]
public class Note : ScriptableObject {
    public Sprite sprite;
    public bool isSign;
}
