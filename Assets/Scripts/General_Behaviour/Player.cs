using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //Inventory Variables
    [SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;
    public GameObject craftableItem;
    private bool ableToCraft = false;
    public GameObject UI_Crafting;
  
    //Movement Variables
    private const float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool dashPressed;
    public float dashCooldown;
    private float nextDash;

    //Player Combat
    public bool ableToAttack = false;
    public float attackCooldown;
    private bool attackPressed;
    private float nextAttack;
    public GameObject Projectile;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    //Player Stats
    public Transform pfHealthBar;
    public HealthSystem healthSystem = new HealthSystem(100);
    private GameHandler gameHandler;

    // Start is called before the first frame update
    private void Start() {
        /**********************
            Player Health Bar
        **********************/
        Transform playerStatus = GameObject.Find("PlayerStatus").transform;
        //Create healthBar for the instance
        Transform healthBarTransform = Instantiate(pfHealthBar, playerStatus);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        /******************
            Other Actions
        ******************/
        //Movement
        rb = this.GetComponent<Rigidbody2D>();
        //Get game handler
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        //Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        if(UI_Crafting == null) {
            UI_Crafting = GameObject.Find("UI_Inventory");
        }
    }

    // Update is called once per frame
    void Update() {
        float moveX = 0f;
        float moveY = 0f;
        List<Item> itemList = inventory.GetItemList();

        //Movement by WASD or Arrow keys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { moveY = +1f; DestroyTutorial(); }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { moveX = -1f; DestroyTutorial(); }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { moveY = -1f; DestroyTutorial(); }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { moveX = +1f; DestroyTutorial(); }

        movement = new Vector3(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextDash) dashPressed = true;//Space to dash
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextAttack) if (ableToAttack) attackPressed = true;//E to Attack
        if (Input.GetKeyDown(KeyCode.C)) if(ableToCraft) craftItem();//C to craft
        if (Input.GetKeyDown(KeyCode.R)) gameHandler.Restart();//R to restart

        //TEMP Inventory Usage, passing player and invoking actions and getting out of index errors    
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.useItem(itemList[0], this);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.useItem(itemList[1], this);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.useItem(itemList[2], this);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.useItem(itemList[3], this);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.useItem(itemList[4], this);
        if (Input.GetKeyDown(KeyCode.Alpha6)) inventory.useItem(itemList[5], this);
        if (Input.GetKeyDown(KeyCode.Alpha7)) inventory.useItem(itemList[6], this);
        if (Input.GetKeyDown(KeyCode.Alpha8)) inventory.useItem(itemList[7], this);
        if (Input.GetKeyDown(KeyCode.Alpha9)) inventory.useItem(itemList[8], this);
        if (Input.GetKeyDown(KeyCode.Alpha0)) inventory.useItem(itemList[9], this);

    }

    private void FixedUpdate(){//Fixed update for dashing and movement
        rb.velocity = movement * moveSpeed;

        if(dashPressed) {//dash action
            float dashSpeed = 5f;
            rb.MovePosition(transform.position + movement * dashSpeed);
            nextDash = Time.time + dashCooldown;
            dashPressed = false;
        }

        if(attackPressed) {//attack action
            spriteRenderer.sprite = newSprite;
            Instantiate(Projectile, transform.position, Quaternion.identity);
            nextAttack = Time.time + attackCooldown;
            attackPressed = false;
        }

        //Check if dead
        if (healthSystem.GetHealth() <= 0){
            gameHandler.ShowGameOver();
            Destroy(gameObject);
        }

    }

    //TEMP Sword crafting TODO: better system, not really expandable
    public void craftItem() {
        if (inventory.SearchItem(Item.ItemType.Stone, 2) && inventory.SearchItem(Item.ItemType.Wood, 1))
        {
            Transform temp = GameObject.Find("CraftingOutput").transform;
            Debug.Log("You have the necessary items");
            inventory.RemoveItem(Item.ItemType.Stone, 2);
            inventory.RemoveItem(Item.ItemType.Wood, 1);
            Instantiate(craftableItem, temp);
        }
        else Debug.Log("You dont have the necessary items");
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

        //crafting in station
        if(other.gameObject.tag == "CraftingStation") {
            Transform canvas = GameObject.Find("Canvas").transform;
            Instantiate(UI_Crafting, canvas);
            ableToCraft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //moving out of the station
        if(other.gameObject.tag == "CraftingStation") {
            GameObject panel = GameObject.Find("UI_Crafting(Clone)");
            Destroy(panel);
            ableToCraft = false;
        }
    }

    private void DestroyTutorial() {//Destroy tutorial on move
        GameObject tutorial = GameObject.Find("Tutorial");
        Destroy(tutorial);
    }
}