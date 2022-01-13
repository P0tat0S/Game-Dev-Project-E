using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float ResourceLife;
    public GameObject DroppedItem;

    private void Start() {
        Instantiate(DroppedItem, transform.position - new Vector3(0f,2f,0f),  Quaternion.identity);
    }

    // Update is called once per frame
    private void Update() {
        ResourceLife -= Time.deltaTime;
        if (ResourceLife <= 0) {
            Destroy(gameObject);
        }
    }
}
