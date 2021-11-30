using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float ResourceLife;


    // Update is called once per frame
    void Update() {
        ResourceLife -= Time.deltaTime;
        if (ResourceLife <= 0)
        {
            Destroy(gameObject);

        }
    }
}
