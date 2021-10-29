using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item {

    //Define all types of items
    public enum ItemType {
        Wood,
        Stone,
        Sword,
        HealthPotion,
        Food,
        Coin,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Wood:             return ItemAssets.Instance.woodSprite;
            case ItemType.Stone:            return ItemAssets.Instance.stoneSprite;
            case ItemType.Sword:            return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:     return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Food:             return ItemAssets.Instance.foodSprite;
            case ItemType.Coin:             return ItemAssets.Instance.coinSprite;
        }
    }

    public bool IsStackable() {
        switch (itemType) {
            default:
            case ItemType.Wood:
            case ItemType.Stone:
            case ItemType.HealthPotion:
            case ItemType.Food:
            case ItemType.Coin:
                return true;
            case ItemType.Sword:
                return false;

        }
    }
}
