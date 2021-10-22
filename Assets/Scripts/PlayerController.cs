using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.InputSystem ;

public class PlayerController : MonoBehaviour {

    public Vector2 moveValue;
    public float speed;
    public float jumpForce;
    private int woodAmount;
    private int stoneAmount;

    void OnMove(InputValue value ) {
        moveValue = value.Get<Vector2>() ;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
        }
    }

    void FixedUpdate() {
        /*if( Keyboard.current.anyKey.wasPressedThisFrame ) { 
            Vector3 jump = new Vector3(0,jumpForce,0);
            GetComponent<Rigidbody2D>().AddForce(jump);
        }*/
        Vector3 movement = new Vector3 ( moveValue.x , 0.0f , 0.0f ) ;
        GetComponent<Rigidbody2D>().AddForce(movement * speed * Time.fixedDeltaTime );
    }
}