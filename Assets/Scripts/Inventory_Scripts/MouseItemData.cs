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
    private GameObject player;
    private GameObject droppedItem;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
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

            //Drop Item --> Left Click
            if (Mouse.current.leftButton.wasReleasedThisFrame && !IsPointerOverUIObject()) {
                for (int i = 0; i < AssignedInventorySlot.StackSize; i++) {
                    DropItem(AssignedInventorySlot);
                }
                ClearSlot();
            }

            //Use Item --> Right Click
            if (Mouse.current.rightButton.wasPressedThisFrame) {
                AssignedInventorySlot.ItemData.UseItem();
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

    public void DropItem(InventorySlot invSlot) {
        //Create Object
        droppedItem = new GameObject(invSlot.ItemData.DisplayName);
        droppedItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.0f);
        droppedItem.AddComponent<BoxCollider2D>();
        droppedItem.AddComponent<SpriteRenderer>();
        droppedItem.AddComponent<ItemPickUp>();

        SpriteRenderer sr = droppedItem.GetComponent<SpriteRenderer>(); 
        sr.sprite= invSlot.ItemData.Icon;

        ItemPickUp ipu = droppedItem.GetComponent<ItemPickUp>();
        ipu.ItemData = invSlot.ItemData;

        //Drop Object
        Instantiate(droppedItem, player.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        Destroy(droppedItem);//Fix for the bug double drop
    }

    public static bool IsPointerOverUIObject() {//From StackOverflow, enable clickable part that is not the item on the mouse
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}