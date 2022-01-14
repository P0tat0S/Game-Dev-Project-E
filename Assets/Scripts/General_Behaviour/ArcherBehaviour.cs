using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviour : MonoBehaviour {
    //Movement variables
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    private GameHandler gameHandler;

    //Enemy Stats
    public float health;
    public Transform pfHealthBar;
    public HealthSystem healthSystem;
    public int scoreOnKill;

    //Projectile stats
    private float TimeBtwShots;
    public float StartTimeBtwShots;
    public GameObject Projectile;
    public bool facingRight = true;
    public Animator animator;
    public GameObject Gold;
    public GameObject Meat;
    public GameObject Coin;
    private bool itemDropped = false;
    private GameObject Resources;

    void Start() { //Start to initialise healthbar and movement
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        /*******************
           Enemy Healthbar
        *******************/
        //Start Health System of the enemy with selected health
        health *= (1 + gameHandler.EnemeyGrowth / 40f);
        healthSystem = new HealthSystem(health);
        //Create the healthbar position it and scale it
        Transform healthBarTransform = Instantiate(pfHealthBar, this.transform.position + new Vector3(-0.1f, 0.6f, 0f), Quaternion.identity, this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        //Player targeting and rigid body init for Movement
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        //Just to tidy Object Hierarchy
        Resources = GameObject.Find("Resources");
    }

    // Update is called once per frame
    void FixedUpdate() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Movement towards player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //compares player position to enemy position and flips if player is facing a different direction
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            Flip();
        }
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            Flip();
        }
        //Shooting/Attacking
        if (TimeBtwShots <= 0) {
            Instantiate(Projectile, transform);
            TimeBtwShots = StartTimeBtwShots;
        } else {
            TimeBtwShots -= Time.deltaTime;
        }

        //Check if dead
        if (healthSystem.GetHealth() <= 0) {
            animator.SetBool("IsDead", true);
            Destroy(gameObject, 0.6f);
            gameHandler.UpdateScore((int)(1 + gameHandler.EnemeyGrowth / 40f) * scoreOnKill);
            //Drop Item from Item List
            DropItem();
        }
    }

    public void Flip()
    {
        //flips the enemy by inversing its movement if it needs to face the other direction
        facingRight = !facingRight;
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }

    public void DropItem()
    {
        if (!itemDropped)
        {
            itemDropped = true;
            float chance = Random.Range(0.0f, 1.0f);
            if (chance < 0.6f)
            {//Spawn Coin with 60% chance
                Instantiate(Coin, transform.position, Quaternion.identity, Resources.transform);
            }
            else if (chance < 0.9f)
            {//Spawn Meat with 30% chance
                Instantiate(Meat, transform.position, Quaternion.identity, Resources.transform);
            }
            else
            { //Spawn Gold with 10% chance 
                Instantiate(Gold, transform.position, Quaternion.identity, Resources.transform);
            }
        }
    }
}
