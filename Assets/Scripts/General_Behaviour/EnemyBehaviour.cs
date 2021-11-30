using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
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

    //Projectile stats
    private float TimeBtwShots;
    public float StartTimeBtwShots;
    public GameObject Projectile;


    void Start() { //Start to initialise healthbar and movement
        /*******************
           Enemy Healthbar
        *******************/
        //Start Health System of the enemy with selected health
        healthSystem = new HealthSystem(health);
        //Create the healthbar position it and scale it
        Transform healthBarTransform = Instantiate(pfHealthBar, this.transform.position + new Vector3(0, 0.8f), Quaternion.identity, this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        //Player targeting and rigid body init for Movement
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate() {
        //Movement towards player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        //Shooting/Attacking
        if (TimeBtwShots <= 0) {
            Instantiate(Projectile, transform);
            TimeBtwShots = StartTimeBtwShots;
        } else {
            TimeBtwShots -= Time.deltaTime;
        }

        //Check if dead
        if (healthSystem.GetHealth() <= 0) {
            Destroy(gameObject);
            var gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.UpdateScore(scoreOnKill);
            gameHandler.dropItem(this.transform);
        }
    }
}
