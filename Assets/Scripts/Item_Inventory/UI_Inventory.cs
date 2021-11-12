using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour {

    //Inventory variables
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform emptySlot;

    private void Awake() {//Initialise to Fetch UI
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        emptySlot = itemSlotContainer.Find("emptySlot");
    }

    public void SetInventory(Inventory inventory) {//Function to initialise Inventory UI
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    //Event handler when Inventory changes
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        //Destroy the images each time inventory changes and re-create them
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            if (child == emptySlot) continue;
            Destroy(child.gameObject);
        }

        int x = 0;//index of slot
        float itemSlotCellSize = 98f;//Fixed Separation of items in inventory

        //Creation of empty itemSlots
        for (int i = 0; i < 10; i++) {
            RectTransform itemSlotRectTransform = Instantiate(emptySlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(i * itemSlotCellSize, 0);
        }

        //Creation of itemSlots with items
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);//The position of the next slot
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();//Change image with item sprite

            //Item text below of the image
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1) {
                uiText.SetText(item.amount.ToString());
            } else {
                uiText.SetText("");
            }

            x++;//Next item slot
        }
    }
}
