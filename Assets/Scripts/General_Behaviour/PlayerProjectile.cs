using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    //Private variables
    private GameObject enemy;
    private Vector2 lastEnemyPosition;

    //Public variables
    public float speed;
    public int damage;
    public float projectileLife;

    
    void Start() { //Start is used to direct the arrow towards the enemy
        //Get position of the enemy for the projectile
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        lastEnemyPosition = enemy.transform.position;

        //Rotate projectile once towards enemy
        Vector3 direction = enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.GetComponent<Rigidbody2D>().rotation = angle;
    }

    
    void FixedUpdate() { //Update to move the arrow and destroy the arrow on miss
        //Move towards the enemys last position
        transform.position = Vector2.MoveTowards(transform.position, lastEnemyPosition, speed * Time.deltaTime);

        //Projectiles is Destroyed after "projectileLife" seconds
        projectileLife -= Time.deltaTime;
        if(projectileLife <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) { //Projectile interaccion on enemy collision
        if (other.CompareTag("Enemy")) {
            var health = other.GetComponent<EnemyBehaviour>().healthSystem;
            health.Damage(damage);
            Destroy(gameObject); 
        }
    }
}
