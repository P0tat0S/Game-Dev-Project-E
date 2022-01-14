using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot {
    [SerializeField] private ItemData itemData; //Reference to item Data
    [SerializeField] private int stackSize; //Current Stack Size(current number of items)

    //Getters
    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    //Inventory Slot constructors
    public InventorySlot(ItemData source, int amount) {//Slot with Item
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() {//Slot with empty slot
        ClearSlot();
    }

    //Inventory Functions
    public void ClearSlot() {//Clears the slot
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) {//Assign an item to the slot
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);//If item already in Inventory, stack it
        else {// Overwrite slot with new invSlot
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(ItemData data, int amount) {//Force Update
        itemData = data;
        stackSize = amount;
    }

    //Function that checks for room in the stack and can return amount remaining
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd) {

        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    //Functions to add and remove from stack
    public void AddToStack(int amount) {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) {
        stackSize -= amount;
        if (stackSize <= 0) ClearSlot();
        var UISlot = GameObject.Find("PlayerInventory").GetComponent<DynamicInventoryDisplay>();
        UISlot.RefreshDynamicInventory(UISlot.InventorySystem);
    }

    public bool SplitStack(out InventorySlot splitStack) {
        if (stackSize <= 1) {//Only split if more than 1
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);//Half stack
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);//Create a copy of the slot with half items.
        return true;
    }
}
