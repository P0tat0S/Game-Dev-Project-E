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
    public bool died = false;
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
    public float damage = 10f;

    public Transform PlayerAttack;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    //Animator component
    public Animator animator;
    public bool facingRight = true;

    //Placeable Objects (Not really scalable)
    public GameObject chest;
    public GameObject campfire;
    //Private variables
    private GameObject enemy;
    private GameObject[] enemies;
    private Vector2 lastEnemyPosition;

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
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        try { enemy = ClosestEnemy(enemies); }
        catch (System.IndexOutOfRangeException) { enemy = GameObject.Find("GameHandler"); } //TEMP fix
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
            nextAttack = Time.time + attackCooldown;
            hungerSystem.Starve(0.1f);
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(PlayerAttack.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemy) {
                if (enemy.CompareTag("Enemy") && enemy.GetComponent<KnightBehaviour>() != null) {
                    var health = enemy.GetComponent<KnightBehaviour>().healthSystem;


                    animator.SetTrigger("LightAttack");
                    health.Damage(damage);
                }

                if (enemy.CompareTag("Enemy") && enemy.GetComponent<ArcherBehaviour>() != null)
                {
                    var health = enemy.GetComponent<ArcherBehaviour>().healthSystem;


                    animator.SetTrigger("LightAttack");
                    health.Damage(damage);
                }

            }
            attackPressed = false;
        }

        //Check if dead
        if ((healthSystem.GetHealth() == 0 || hungerSystem.GetHunger() == 0) && !died){
            animator.SetTrigger("Dead");
            Destroy(gameObject, 0.8f);
            died = true;
            var endMenu = GameObject.Find("End").GetComponent<EndMenu>();
            endMenu.IsDead();
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
    //Function that returns the closest enemy from a enemy array
    private GameObject ClosestEnemy(GameObject[] enemies) {
        var closestEnemy = enemies[0];
        float lowestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies) {
            Vector3 vectorDifference = enemy.transform.position - transform.position;
            float distanceBtwn = vectorDifference.sqrMagnitude;
            if (distanceBtwn < lowestDistance) {
                closestEnemy = enemy;
                lowestDistance = distanceBtwn;
            }
        }
        return closestEnemy;
    }
}
