using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject {
    public List<InventorySlot> Materials;
    public List<InventorySlot> Results;

    private bool AbleToCraft(PlayerInventoryHolder playerInventory) {
        foreach (InventorySlot craftingItems in Materials) {
            if (playerInventory.ItemAmount(craftingItems.ItemData, craftingItems.StackSize)) 
            return false;
        }
        return true;
    }

    public void Craft(PlayerInventoryHolder playerInventory) {
        if (AbleToCraft(playerInventory)) {
            foreach (InventorySlot craftingItems in Materials) {
                playerInventory.RemoveItem(craftingItems.ItemData, craftingItems.StackSize);
            }

            foreach (InventorySlot craftedItem in Results) {
                playerInventory.AddToInventory(craftedItem.ItemData, craftedItem.StackSize);
            } 
        }
    }
}
 