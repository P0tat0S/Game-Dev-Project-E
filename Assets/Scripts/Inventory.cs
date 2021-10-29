using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    private List<Item> itemList;

    //Initialise Inventory
    public Inventory() {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Food, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Food, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Food, amount = 1});
        Debug.Log(itemList.Count);
    }

    /*********************
        Helper Functions
    **********************/
    public void AddItem(Item item) {
        itemList.Add(item);
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
