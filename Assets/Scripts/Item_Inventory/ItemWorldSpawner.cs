using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour {
    
    public Item item;
    
    //ItemWorld Spawner uses the Itemworlds function to spawn an Item and dissapears
    private void Start() {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
