using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour {
    
    public Item item;
    public bool isPickup = true;
    private Vector3 spawnPosition;
    
    //ItemWorld Spawner uses the Itemworlds function to spawn an Item and dissapears
    private void Start() {
        if(!isPickup) spawnPosition = transform.position + new Vector3(0,-2.0f);
        else spawnPosition = transform.position;
        ItemWorld.SpawnItemWorld(spawnPosition, item);
        if(isPickup){
            Destroy(gameObject);
        }
    }
}
