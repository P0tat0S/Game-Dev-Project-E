using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //Inventory Variables
    [SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;
    public GameObject craftableItem;
    private bool ableToCraft;
    public GameObject UI_Crafting;
  
    //Movement Variables
    private const float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool dashPressed;
    public float dashCooldown;
    private float nextDash;

    //Player Stats
    [SerializeField] private HealthBar healthBar;

    //TEMP player stats
    private int health = 100;
    private int hunger = 20;

    //TEMP player actions
    private void receiveDamage(){//Action to receive damage
        health -= Random.Range(1,10);
        Debug.Log("You got hit \n"+"Player Health: " + health);
    }

    private void receiveHunger(){//Action to get hungry
        hunger -= Random.Range(1,2);
        Debug.Log("You get hungry \n"+"Player Hunger: " + hunger);
    }

    public void heal() {
        health += Random.Range(50,60);
        Debug.Log("You HEAL \n"+"Player Health: " + health);
    }

    public void eat() {
        hunger += Random.Range(10,12);
        Debug.Log("You EAT \n"+"Player Hunger: " + hunger);
    }

    //Sword crafting TODO: better system, current system requires knowing the enum of items
    public void craftItem() {
        if(inventory.SearchItem(1, 2) && inventory.SearchItem(0, 1)) {
            Transform temp = GameObject.Find("CraftingOutput").transform;
            Debug.Log("You have the necessary items");
            inventory.RemoveItem(1,2);
            inventory.RemoveItem(0,1);
            Instantiate(craftableItem, temp);
        } else {
            Debug.Log("You dont have the necessary items");
        }
    }

    // Start is called before the first frame update
    private void Start() {
        //Movement
        rb = this.GetComponent<Rigidbody2D>();
        //Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        Debug.Log("Player Health: " + health);
        Debug.Log("Player Hunger: " + hunger);
        ableToCraft = false;
    }

    // Update is called once per frame
    void Update() {
        float moveX = 0f;
        float moveY = 0f;
        List<Item> itemList = inventory.GetItemList();

        //Movement
        if (Input.GetKey(KeyCode.W)){
          moveY = +1f;
        }
        if (Input.GetKey(KeyCode.A)){
          moveX = -1f;
        }
        if (Input.GetKey(KeyCode.S)){
          moveY = -1f;
        }
        if (Input.GetKey(KeyCode.D)){
          moveX = +1f;
        }

        movement = new Vector3(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextDash){
          dashPressed = true;
        }
        
        //TEMP Inventory Usage       
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            inventory.useItem(itemList[0], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            inventory.useItem(itemList[1], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            inventory.useItem(itemList[2], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            inventory.useItem(itemList[3], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            inventory.useItem(itemList[4], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            inventory.useItem(itemList[5], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            inventory.useItem(itemList[6], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            inventory.useItem(itemList[7], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            inventory.useItem(itemList[8], this);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            inventory.useItem(itemList[9], this);
        }

        //TEMP Inventory and Player interactions
        if (Input.GetKeyDown(KeyCode.E)) {
            this.receiveDamage();
            this.receiveHunger();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            if(ableToCraft) craftItem();
        }
    }

    private void FixedUpdate(){
        rb.velocity = movement * moveSpeed;

        if (dashPressed){
          float dashSpeed = 5f;
          rb.MovePosition(transform.position + movement * dashSpeed);
          nextDash = Time.time + dashCooldown;
          dashPressed = false;
        }

    }

    //Pick UP Items
    private void OnTriggerEnter2D(Collider2D other) {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if(itemWorld != null) {
            //Touching Item
            other.gameObject.SetActive(false);
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }

        //crafting in
        if(other.gameObject.tag == "CraftingStation") {
            Instantiate(UI_Crafting);
            ableToCraft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //crafting out
        if(other.gameObject.tag == "CraftingStation") {
            GameObject panel = GameObject.Find("UI_Crafting");
            Destroy(panel);
            ableToCraft = false;
        }
    }
}