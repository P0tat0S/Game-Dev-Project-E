using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.InputSystem ;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Vector2 moveValue;
    public float speed;
    public float jumpForce;
    private Inventory inventory;
    private int woodAmount;
    private int stoneAmount;
    public Text woodCount;
    public Text stoneCount;
    public Canvas craftinUI; 

    void Start() {
        woodAmount = 0;
        stoneAmount = 0;
        SetCountText();
        inventory = new Inventory();
    }

    void OnMove(InputValue value ) {
        moveValue = value.Get<Vector2>() ;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            if(other.transform.parent.gameObject.tag == "WoodPickUp") {
                woodAmount ++;
                //Debug.Log("Wood Picked up");
                SetCountText();
            } else if (other.transform.parent.gameObject.tag == "StonePickUp") {
                stoneAmount ++;
                //Debug.Log("Stone Picked up");
                SetCountText();
            }
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

    private void SetCountText() {
        woodCount.text = "Wood Count: " + woodAmount.ToString();
        stoneCount.text = "Stone Count: " + stoneAmount.ToString();
    }
}