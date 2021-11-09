using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float DamageTaken;
    public float MaxDamageTaken = 5;

    private float TimeBtwShots;
    public float StartTimeBtwShots;
    public GameObject Projectile;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        DamageTaken = MaxDamageTaken;
        //HealthBar.SetHealth(DamageTaken, MaxDamageTaken);

    }

    public void TakeHit(float HitPoint)
    {
        DamageTaken = Mathf.Max(0, DamageTaken - HitPoint);

        //      DamageTaken -= HitPoint;
        //     HealthBar.SetHealth(DamageTaken, MaxDamageTaken);

        if (DamageTaken == 0)
        {
            Destroy(gameObject);
        }
    }
    public float GetDamage()
    {
        return DamageTaken;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction = -direction;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        if(TimeBtwShots <= 0)
        {
            Instantiate(Projectile, transform.position, Quaternion.identity);
            TimeBtwShots = StartTimeBtwShots;

        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
        //HealthBar.SetHealth(DamageTaken, MaxDamageTaken);
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}