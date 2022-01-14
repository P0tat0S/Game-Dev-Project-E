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
    public Sprite Icon;
    public int MaxStackSize;
    public int GoldValue;
    private Player player;

    //Function to Use Items
    public void UseItem() {
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        switch (DisplayName) {
            case "Armor Upgrade":
                player.AddArmor(5f);
                break;
            case "Armor":
                player.EquipArmor();
                break;
            case "Campfire":
            case "Chest":
                player.PlaceObject(DisplayName);
                break;
            case "Cooked Meat":
                player.hungerSystem.Eat(100f);
                break;
            case "Grapes":
                player.hungerSystem.Eat(10f);
                break;
            case "Meat":
                player.hungerSystem.Eat(25f);
                break;
            case "Potion":
                player.healthSystem.Heal(25f);
                break;
            case "Sword Upgrade":
                player.AddDamage(10f);
                break;
            case "Sword":
                player.EquipSword();
                break;
            default:
                return;
        }
    }
}
