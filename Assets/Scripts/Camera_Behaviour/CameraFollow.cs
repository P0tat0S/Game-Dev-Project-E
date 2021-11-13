using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //Variables for a smooth camera
    private Transform playerTracked;
    private float xOffset, yOffset, zOffset = -1.0f;
    public float followSpeed;

    //Varoables for camera zoom
    private Camera thisCamera;
    public float zoomSpeed;
    private float zoom;

    private void Start() {//Fetch Player and Camera
        thisCamera = GetComponent<Camera>();
        playerTracked = GameObject.FindGameObjectWithTag("Player").transform;
        zoom = thisCamera.orthographicSize;
    }

    private void FixedUpdate() {//Fixed Update for smooth camera movement
        //Smoothing of camera movement
        float xPlayer = playerTracked.position.x + xOffset;
        float yPlayer = playerTracked.position.y + yOffset;
        float xNewPosition = Mathf.Lerp(transform.position.x, xPlayer, Time.deltaTime * followSpeed);
        float yNewPosition = Mathf.Lerp(transform.position.y, yPlayer, Time.deltaTime * followSpeed);
        transform.position = new Vector3(xNewPosition, yNewPosition, playerTracked.position.z + zOffset);

        //Smoothing camera zoom
        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);
    }

    private void Update() {//Update for Scroll wheel input
        zoom -= Input.mouseScrollDelta.y * 0.5f;
        if(zoom >= 16.0f) {
            zoom = 16.0f;
        } else if(zoom <= 8.0f) {
            zoom = 8.0f;
        }
    }
}
