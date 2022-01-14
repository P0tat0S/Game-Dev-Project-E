using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    //Movement Variables
    public Vector2 moveValue;
    private const float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool dashPressed;
    public float dashCooldown;
    private float nextDash;

    //Player Combat
    public float attackCooldown;
    private bool attackPressed;
    private float nextAttack;
    public GameObject Projectile;
    private float defense = 0f;
    private bool armorEquiped = false;
    public float Defense => defense;

    //Player Stats
    public Transform pfHealthBar;
    public Transform pfHungerBar;
    public HealthSystem healthSystem = new HealthSystem(100);
    private GameHandler gameHandler;
    public HungerSystem hungerSystem = new HungerSystem(50);

    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    //Animator component
    public Animator animator;
    public bool facingRight = true;

    //Placeable Objects (Not really scalable)
    public GameObject chest;
    public GameObject campfire;

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

        /**********************
            Player Hunger Bar
        **********************/
        Transform hungerBarTransform = Instantiate(pfHungerBar, playerStatus);
        //Create hungerBar for the instance
        hungerBarTransform.position = hungerBarTransform.position + new Vector3(0f,-0.25f);
        HungerBar hungerBar = hungerBarTransform.GetComponent<HungerBar>();
        hungerBar.Setup(hungerSystem);

        /******************
            Other Actions
        ******************/
        //Movement
        rb = this.GetComponent<Rigidbody2D>();
        //Get game handler
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    /***********
        Inputs
    ***********/
    void OnMove(InputValue value) { 
        moveValue = value.Get<Vector2>(); DestroyTutorial();
        animator.SetFloat("Speed", Mathf.Abs(moveValue.magnitude));
        if(moveValue.x < 0 && facingRight) {
            Flip();
        } else if (moveValue.x > 0 && !facingRight) {
            Flip();
        }
    }
    void OnDash() { if (Time.time > nextDash) dashPressed = true; }
    void OnAttack() { if (Time.time > nextAttack) attackPressed = true; }
    void OnReset() { gameHandler.Restart(); }
    void OnHotbar() {} //1-9

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate(){//Fixed update for dashing and movement
        Vector3 movement = new Vector3(moveValue.x, moveValue.y, 0f);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        if(dashPressed) {//dash action
            float dashSpeed = 2.5f;
            rb.MovePosition(transform.position + movement * dashSpeed);
            nextDash = Time.time + dashCooldown;
            hungerSystem.Starve(0.5f);
            dashPressed = false;
        }

        if(attackPressed) {//attack action
            Instantiate(Projectile, transform);
            nextAttack = Time.time + attackCooldown;
            hungerSystem.Starve(0.1f);
            attackPressed = false;
        }

        //Check if dead
        if (healthSystem.GetHealth() == 0 || hungerSystem.GetHunger() == 0){
            gameHandler.ShowGameOver();
            Destroy(gameObject);
        }
    }

    //TEMP
    private void DestroyTutorial() {//Destroy tutorial on move
        GameObject tutorial = GameObject.Find("Tutorial");
        Destroy(tutorial);
    }

    public void Flip() {
        //flips the enemy by inversing if it needs to face the other direction
        facingRight = !facingRight;
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }

    public void EquipArmor() {
        //Can only be equipped once
        if(!armorEquiped) {
            armorEquiped = true;
            defense += 20f;
        }
    }
    public void AddArmor(float defenseToAdd) {
        //Just Add Armor
        defense += defenseToAdd;
    }
    
    public void EquipSword() {
        //Equip Weapon to Attack with more damage
    }
    public void AddDamage(float attack) {
        //Just Add Damage to Sword
    }

    public void PlaceObject(string objectToPlace) {

    }
}
