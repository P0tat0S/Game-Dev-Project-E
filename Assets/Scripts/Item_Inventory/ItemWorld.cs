using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour {

    //Item variables
    private Item item;
    private SpriteRenderer spriteR;
    private TextMeshPro textMeshPro;

    //Function for later usage to spawn items
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private void Awake() {//Item World fetch of sprite and amount text
        spriteR = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    /***********************************
        Helper Functions for ItemWorld
    ***********************************/
    public void SetItem(Item item) {
        this.item = item;
        spriteR.sprite = item.GetSprite();
        if (item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        } else {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}