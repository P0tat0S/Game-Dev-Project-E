using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem {
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size) {//Constructor that sets the amount of slots
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++) {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(ItemData itemToAdd, int amountToAdd) {
        //Checks if item already is in inventory
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) {//returns a list of the items
            foreach (var slot in invSlot) {
                if(slot.RoomLeftInStack(amountToAdd)) {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        } 
        
        if (HasFreeSlot(out InventorySlot freeSlot)) {//Returns free slot
            if (freeSlot.RoomLeftInStack(amountToAdd)) {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(ItemData itemToAdd, out List<InventorySlot> invSlot) {//Check if items exists in inventory
        invSlot = inventorySlots.Where(slot => slot.ItemData == itemToAdd).ToList();//Get a list
        return invSlot == null ? false : true;//If it does returns true and returns a slot
    }

    public bool HasFreeSlot(out InventorySlot freeSlot) {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null);//Get first free slot
        return freeSlot == null ? false : true;
    }
}
