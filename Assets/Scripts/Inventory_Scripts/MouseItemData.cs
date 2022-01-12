using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class MouseItemData : MonoBehaviour {

    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private void Awake() {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot) {//Assigns mouse with itemData properties
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update() { //If mouse has item, follow the mouse
        if (AssignedInventorySlot.ItemData != null) {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject()) {
                //TODO: Drop items in the ground
                
                ClearSlot();
            }
        }
    }

    public void ClearSlot() {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject() {//From StackOverflow, enable clickable part that is not the item on the mouse
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}