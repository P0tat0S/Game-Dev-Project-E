using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {
    private Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    //Enemy Stats
    public Transform pfHealthBar;
    public HealthSystem healthSystem = new HealthSystem(20);

    // Start is called before the first frame update
    void Start() {
        /*****************
            Enemy Stats
        ******************/
        //Create healthBar for the instance
        Transform healthBarTransform = Instantiate(pfHealthBar, this.transform.position + new Vector3(0, 1.6f), Quaternion.identity, this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        //Enemy Movement
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;

    }
    private void FixedUpdate() {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
