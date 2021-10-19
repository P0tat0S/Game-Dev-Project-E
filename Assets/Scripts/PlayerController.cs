using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.InputSystem ;

public class PlayerController : MonoBehaviour {

    public Vector2 moveValue;
    public float speed;
    public float jumpForce;

    void OnMove(InputValue value ) {
        moveValue = value.Get<Vector2>() ;
    }

    void FixedUpdate() {
        if( Keyboard.current.anyKey.wasPressedThisFrame ) { 
            Vector3 jump = new Vector3(0,jumpForce,0);
            GetComponent<Rigidbody>().AddForce(jump);
        }
        Vector3 movement = new Vector3 ( moveValue.x , 0.0f , 0.0f ) ;
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime );
    }
}