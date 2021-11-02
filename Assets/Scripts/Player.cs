using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //Inventory Variables
    [SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;
  
    //Movement Variables
    private const float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool dashPressed;

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

    // Start is called before the first frame update
    private void Start() {
        //Movement
        rb = this.GetComponent<Rigidbody2D>();
        //Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        Debug.Log("Player Health: " + health);
        Debug.Log("Player Hunger: " + hunger);
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

        if (Input.GetKeyDown(KeyCode.Space)){
          dashPressed = true;
        }
        
        //TEMP Inventory Usage       
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            itemList[0].useItem(this, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            itemList[1].useItem(this, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            itemList[2].useItem(this, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            itemList[3].useItem(this, 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            itemList[4].useItem(this, 4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            itemList[5].useItem(this, 5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            itemList[6].useItem(this, 6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            itemList[7].useItem(this, 7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            itemList[8].useItem(this, 8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            itemList[9].useItem(this, 9);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            this.receiveDamage();
            this.receiveHunger();
        }
    }

    private void FixedUpdate(){
        rb.velocity = movement * moveSpeed;

        if (dashPressed){
          float dashSpeed = 5f;
          rb.MovePosition(transform.position + movement * dashSpeed);
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
    }
}