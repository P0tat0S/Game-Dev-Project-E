using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Vector2 lastPlayerPosition;
    private GameObject player;
    public int damage;
    public float projectileLife;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;   
    }

    // Update is called once per frame
    void Update() {
        //Position
        transform.position = Vector2.MoveTowards(transform.position, lastPlayerPosition, speed * Time.deltaTime);
        projectileLife -= Time.deltaTime;
        if(projectileLife <= 0) {
            Destroy(gameObject);
        }

        /*Vector3 direction = lastPlayerPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.GetComponent<Rigidbody2D>().rotation = angle;*/
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var health = player.GetComponent<Player>().healthSystem;
            health.Damage(damage);
            Destroy(gameObject); 
        }
    }

}
