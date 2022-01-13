using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : MonoBehaviour
{
    //Movement variables
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed; 

    //Enemy Stats
    public float health;
    public Transform pfHealthBar;
    public HealthSystem healthSystem;
    public int scoreOnKill;
    public float damage;
    //Enemy attack
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    //Animator component
    public Animator animator;
    //used to check is sprite needs flipping
    public bool facingRight = true;


    void Start()
    { //Start to initialise healthbar and movement
        /*******************
           Enemy Healthbar
        *******************/
        //Start Health System of the enemy with selected health
        healthSystem = new HealthSystem(health);
        //Create the healthbar position it and scale it
        Transform healthBarTransform = Instantiate(pfHealthBar, this.transform.position + new Vector3(-0.3f, 0.6f, 0f), Quaternion.identity, this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        //Player targeting and rigid body init for Movement
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Movement towards player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //compares player position to enemy position and flips if player is facing a different direction
        if (player.transform.position.x < gameObject.transform.position.x && !facingRight) {
            Flip();
        }
        if (player.transform.position.x > gameObject.transform.position.x && facingRight) {
            Flip();
        }
        //plays the move animation if the enemy is not attacking
        animator.SetBool("Attack", false);
        //detects player in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, playerLayers);
        var health = player.GetComponent<Player>().healthSystem;
        foreach(Collider2D player in hitPlayer)
        {
            //plays the attack animation when in range to attack player
            animator.SetBool("Attack", true);
            //damages players health
            health.Damage(damage);
        }

        //Check if dead
        if (healthSystem.GetHealth() <= 0)
        {
            animator.SetBool("IsDead", true);
            Destroy(gameObject);
            var gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.UpdateScore(scoreOnKill);
            gameHandler.dropItem(this.transform);
        }
    }

    public void Flip()
    {
        //flips the enemy by inversing if it needs to face the other direction
        facingRight = !facingRight;
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }
}
