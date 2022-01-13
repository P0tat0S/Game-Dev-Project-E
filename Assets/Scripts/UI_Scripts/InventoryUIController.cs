using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour {

    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerInventoryPanel;
    public CraftingDisplay craftingPanel;

    private void Awake() {
        chestPanel.gameObject.SetActive(false);
        playerInventoryPanel.gameObject.SetActive(false);
        craftingPanel.gameObject.SetActive(true);
    }

    private void OnEnable() {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnDynamicPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable() {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnDynamicPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    void Update() {
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            chestPanel.gameObject.SetActive(false);
        if (playerInventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            playerInventoryPanel.gameObject.SetActive(false);
        if (craftingPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            craftingPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay) {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerInventory(InventorySystem invToDisplay){
        playerInventoryPanel.gameObject.SetActive(true);
        playerInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayCrafting(InventorySystem invToDisplay) {
        craftingPanel.gameObject.SetActive(true);
    }

}
