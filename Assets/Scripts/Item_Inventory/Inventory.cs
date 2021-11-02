using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    //Initialise Inventory
    public Inventory() {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Food, amount = 1});
    }

    /*********************
        Helper Functions
    **********************/
    public void AddItem(Item item) {
        if (item.IsStackable()) {
            bool alreadyInInventory = false;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount += item.amount;
                    alreadyInInventory = true;
                }
            }
            if(!alreadyInInventory) {
                itemList.Add(item);
            }
        } else {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item) {
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == item.itemType) {
                inventoryItem.amount -= 1;
            }
            if (inventoryItem.amount == 0) {
                itemList.Remove(item);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
