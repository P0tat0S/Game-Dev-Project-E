using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PlayerInventoryHolder : InventoryHolder {
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicPlayerInventoryDisplayRequested;

    protected override void Awake() {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }

    void Update() {
        if (Keyboard.current.iKey.wasPressedThisFrame) OnDynamicPlayerInventoryDisplayRequested?.Invoke(secondaryInventorySystem);
    }

    public bool AddToInventory(ItemData data, int amount) {
        if (primaryInventorySystem.AddToInventory(data, amount)) return true;
        else if (secondaryInventorySystem.AddToInventory(data, amount)) return true;
        return false;
    }

    public void RemoveItem(ItemData data, int amount) {
        InventorySlot itemToRemove = secondaryInventorySystem.InventorySlots
        .Where(slot => slot.ItemData == data)
        .Where(slot => slot.StackSize >= amount)
        .FirstOrDefault();
        itemToRemove.RemoveFromStack(amount);
    }

    public bool ItemAmount(ItemData itemData, int itemAmount) {
        List<InventorySlot> invSlot = secondaryInventorySystem.InventorySlots
        .Where(slot => slot.ItemData == itemData)
        .Where(slot => slot.StackSize <= itemAmount)
        .ToList();
        return invSlot == null ? false : true;
    }
}
