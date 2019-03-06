using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level Type", menuName = "Create Level Selector / New Type")]
public class LevelSelector : ScriptableObject {
    public new string LevelType;
    public Sprite[] ButtonImage = new Sprite[100]; 
}
