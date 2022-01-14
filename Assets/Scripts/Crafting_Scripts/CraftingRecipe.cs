using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject {
    public List<InventorySlot> Materials;
    public List<InventorySlot> Results;

    public bool AbleToCraft(PlayerInventoryHolder playerInventory) {
        foreach (InventorySlot craftingItems in Materials) {
            if (playerInventory.ItemAmount(craftingItems.ItemData, craftingItems.StackSize)) {
                //Debug.Log("Crafting not passed");
                return false;
            }
        }
        //Debug.Log("Crafting passed");
        return true;
    }

    public void Craft(PlayerInventoryHolder playerInventory) {
        if (AbleToCraft(playerInventory)) {
            //Debug.Log("Crafting in progress");
            foreach (InventorySlot craftingItems in Materials) {
                playerInventory.RemoveItem(craftingItems.ItemData, craftingItems.StackSize);
            }

            foreach (InventorySlot craftedItem in Results) {
                playerInventory.AddToInventory(craftedItem.ItemData, craftedItem.StackSize);
            } 
        }
    }
}
 