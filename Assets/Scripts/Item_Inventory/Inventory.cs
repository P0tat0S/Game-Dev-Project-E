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
    public List<Item> GetItemList() {
        return itemList;
    }
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

    //Method that removes an item by iterating through the inventory and finding the item
    public void RemoveItem(Item item) {
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == item.itemType) {
                inventoryItem.amount -= 1;
            }
            if (inventoryItem.amount == 0) {
                itemList.Remove(inventoryItem);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    //Overloaded version of remove item by passing the specific item and the amount to remove
    public void RemoveItem(int type, int amount) {
        foreach (Item inventoryItem in itemList) {
            if ((int)inventoryItem.itemType == type) {
                inventoryItem.amount -= amount;
            }
            if (inventoryItem.amount <= 0) {
                itemList.Remove(inventoryItem);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool SearchItem(int type, int amount) {
        foreach (Item inventoryItem in itemList) {
            if ((int) inventoryItem.itemType == type) {
                Debug.Log("Item found");
                if(inventoryItem.amount >= amount) {
                    Debug.Log("Item sufficient");
                    return true;
                } else {
                    Debug.Log(inventoryItem.itemType + 
                        " Amount in inventory: " + inventoryItem.amount +
                        " Required: " + amount + 
                        " Requires more items"
                    );
                    return false;
                }
            }
        }
        return false;
    }

    public void useItem(Item item, Player player) {
        switch ((int)item.itemType) {
            default:
            case 3:
                player.heal();
                RemoveItem(item);
                break;
            case 4:
                player.eat();
                RemoveItem(item);
                break;
            case 2:
            case 0:
            case 1:
            case 5:
                Debug.Log("The item selected has no interaction yet");
                return;
        }
    }
}
