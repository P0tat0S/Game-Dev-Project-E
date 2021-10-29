using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //Inventory Variables
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
  
    //Movement Variables
    private const float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool dashPressed;
    

    // Start is called before the first frame update
    private void Start() {
        //Movement
        rb = this.GetComponent<Rigidbody2D>();
        //Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    // Update is called once per frame
    void Update() {
        float moveX = 0f;
        float moveY = 0f;
        List<Item> itemList = inventory.GetItemList();

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

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log(itemList[0].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log(itemList[1].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log(itemList[2].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            Debug.Log(itemList[3].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            Debug.Log(itemList[4].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            Debug.Log(itemList[5].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            Debug.Log(itemList[6].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            Debug.Log(itemList[7].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            Debug.Log(itemList[8].amount);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            Debug.Log(itemList[9].amount);
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