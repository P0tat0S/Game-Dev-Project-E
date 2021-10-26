using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    //Define all types of items
    public enum ItemType {
        Wood,
        Stone,
        Weapon,
        HealthPotion,
        Food,
        Coin,
    }

    public ItemType itemType;
    public int amount;
}
