using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    //Private variables
    private GameObject player;
    private Vector2 lastPlayerPosition;

    //Public variables
    public float speed;
    public int damage;
    public float projectileLife;

    
    void Start() { //Start is used to direct the arrow towards the player
        //Get position of the player for the projectile
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPosition = player.transform.position;

        //Rotate projectile once towards player
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.GetComponent<Rigidbody2D>().rotation = angle;
    }

    
    void FixedUpdate() { //Update to move the arrow and destroy the arrow on miss
        //Move towards the players last position
        transform.position = Vector2.MoveTowards(transform.position, lastPlayerPosition, speed * Time.deltaTime);

        //Projectiles is Destroyed after "projectileLife" seconds
        projectileLife -= Time.deltaTime;
        if(projectileLife <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) { //Projectile interaccion on player collision
        if (other.CompareTag("Player")) {
            var health = player.GetComponent<Player>().healthSystem;
            health.Damage(damage);
            Destroy(gameObject); 
        }
    }

}
