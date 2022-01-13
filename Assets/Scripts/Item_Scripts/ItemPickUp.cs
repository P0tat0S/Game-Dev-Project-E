using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickUp : MonoBehaviour {
    public ItemData ItemData;

    private Collider2D myCollider;

    private void Awake() {
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1)) {
            Destroy(this.gameObject);
        }
    }
}
