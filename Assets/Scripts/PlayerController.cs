using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private const float moveSpeed = 10f;

  private Rigidbody2D rb;
  private Vector3 movement;
  private bool dashPressed;

  // Start is called before the first frame update
  private void Start(){
      rb = this.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update(){
      float moveX = 0f;
      float moveY = 0f;

      if (Input.GetKey(KeyCode.W)){
        moveY = +1f;
      }

      if (Input.GetKey(KeyCode.A)){
        moveX = -1f;
      }

      if (Input.GetKey(KeyCode.S)){
        moveY = -1f;
      }

      if (Input.GetKey(KeyCode.D)){
        moveX = +1f;
      }

      movement = new Vector3(moveX, moveY).normalized;

      if (Input.GetKeyDown(KeyCode.Space)){
        dashPressed = true;
      }
  }

  private void FixedUpdate(){
      rb.velocity = movement * moveSpeed;

      if (dashPressed){
        float dashSpeed = 5f;
        rb.MovePosition(transform.position + movement * dashSpeed);
        dashPressed = false;
      }
  }
}
