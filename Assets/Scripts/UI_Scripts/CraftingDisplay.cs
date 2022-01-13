using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingDisplay : MonoBehaviour {
    public List<GameObject> recipeDisplay;
    public List<CraftingRecipe> recipes;
    //TODO Pair recipe with recipeDisplay using a dictionary

    public void DisplayRecipes() {
        foreach (var recipe in recipeDisplay) {
            Instantiate(recipe);
        }
    }
}

/*public class CraftingDisplay : InventoryDisplay {
    [SerializeField] protected InventorySlot_UI slotPrefab;
    public List<CraftingRecipe> recipes;

    protected override void Start() {
        base.Start();
    }

    public override void AssignSlot(InventorySystem invToDisplay) {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize; i++) {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay) {
        ClearSlots();
        inventorySystem = invToDisplay;
        if (inventorySystem != null) inventorySystem.OnInventorySlotChanged += UpdateSlots;
        AssignSlot(invToDisplay);
    }

    private void ClearSlots() {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }

    private void OnDisable() {
        if (inventorySystem != null) inventorySystem.OnInventorySlotChanged -= UpdateSlots;
    }
}*/