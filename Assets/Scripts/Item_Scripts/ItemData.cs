using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************************
    This is a scriptable object that gives the properties of the items in the game.
    It contains the base properties that will be inherited for different items.
************************************************************************************/

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class ItemData : ScriptableObject {
    //Base Properties
    public int ID;
    public string DisplayName;
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int GoldValue;
}
