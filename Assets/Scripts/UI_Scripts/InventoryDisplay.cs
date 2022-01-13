using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour {
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;//Pair Up UI slots with system slots in dictionary

    //Getters
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start() {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);//Implemented on child classes

    protected virtual void UpdateSlots(InventorySlot updatedSlot) {
        foreach (var slot in SlotDictionary) {// Slot value
            if (slot.Value == updatedSlot) {
                slot.Key.UpdateUISlot(updatedSlot);// Slot key
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot) {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;//Hard coded split Shift
        //Clicked slot has item - mouse empty --> Pickup Item.
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null) {
            //If player is holding shift key ? Split Stack
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            } else {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        //Clicked slot empty -  mouse has item --> Place Item.
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null) {

            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        //Both have items --> If statement
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null) {
            //Are both items the same? Combine items.
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;
            bool roomInStack = clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack);
            if (isSameItem && roomInStack){
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            } else if (isSameItem && !roomInStack) { //Is the stack size of the combined items > Max Stack size, If so take from mouse
                if (leftInStack < 1) SwapSlot(clickedUISlot); //Stack is full so swap items
                else { //Slot is not at max, take from mouse inventory
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            } else if (!isSameItem) { //Are both Different Items ? Swap the items.
                SwapSlot(clickedUISlot);
                return;
            }
        }
            
    }

    private void SwapSlot(InventorySlot_UI clickedUISlot) {
        var tempSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();
        
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(tempSlot);
        clickedUISlot.UpdateUISlot();
    }
}
