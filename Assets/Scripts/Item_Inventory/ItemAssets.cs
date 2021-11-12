using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {

    /*************************************
        Script to assign sprites to items
    *************************************/
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite woodSprite;
    public Sprite stoneSprite;
    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite foodSprite;
    public Sprite coinSprite;

}
