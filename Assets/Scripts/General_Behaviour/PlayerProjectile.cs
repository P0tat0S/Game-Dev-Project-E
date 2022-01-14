using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    //Private variables
    private GameObject enemy;
    private GameObject[] enemies;
    private Vector2 lastEnemyPosition;

    //Public variables
    public float speed;
    public float damage = 10f;
    public float projectileLife;

    
    
    private void Start() { //Start is used to direct the arrow towards the enemy
        //Get position of the enemy for the projectile
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        try { enemy = ClosestEnemy(enemies); }
        catch (System.IndexOutOfRangeException) { enemy = GameObject.Find("GameHandler"); } //TEMP fix
        lastEnemyPosition = enemy.transform.position;

        //Rotate projectile once towards enemy
        Vector3 direction = enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.GetComponent<Rigidbody2D>().rotation = angle;
    }
    
    private void FixedUpdate() { //Update to move the arrow and destroy the arrow on miss
        //Move towards the enemys last position
        transform.position = Vector2.MoveTowards(transform.position, lastEnemyPosition, speed * Time.deltaTime);

        //Projectiles is Destroyed after "projectileLife" seconds
        projectileLife -= Time.deltaTime;
        if(projectileLife <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //Projectile interaccion on enemy collision
        if (other.CompareTag("Enemy") && other.GetComponent<KnightBehaviour>() != null) {
            var health = other.GetComponent<KnightBehaviour>().healthSystem;
            health.Damage(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") && other.GetComponent<ArcherBehaviour>() != null)
        {
            var health = other.GetComponent<ArcherBehaviour>().healthSystem;
            health.Damage(damage);
            Destroy(gameObject);
        }
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
