using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour {
    Dictionary<int, GameObject> tileset;//Pair id code with Tile Prefab
    Dictionary<int, GameObject> tile_groups;//Just to organise in hierarchy
    //Tiles
    public GameObject tile_water;
    public GameObject tile_dirt;
    public GameObject tile_grass;
    public GameObject tile_grass2;
    //Map Variables
    private const int map_width =  256;
    private const int map_height = 256;

    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

    //Recommended value 4-20
    float magnification = 18.0f;

    //Random "Seed" in each generation
    int x_offset;//Decrease moves left, Increase moves right
    int y_offset;//Decrease moves down, Increase moves up

    void Start() {
        //Center the map to the middle
        transform.position -= new Vector3(map_width/2,map_height/2,-1);
        //Random "Seed" in each generation
        x_offset = Random.Range(-1000, 1000);
        y_offset = Random.Range(-1000, 1000);
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
    }

    //Creation of tilest dictionary and assigns ID code to prefabs, ordered by elevation
    private void CreateTileset() {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, tile_water);
        tileset.Add(1, tile_dirt);
        tileset.Add(2, tile_grass);
        tileset.Add(3, tile_grass2);
    }

    //Sets the tiles in organised groups in hierarchy
    private void CreateTileGroups() {
        tile_groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset) {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0,0,0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    //Function that creates a 2D grid using Perlin noise and stores the ID of the tile gameObject
    private void GenerateMap() { 
        for(int x = 0; x < map_width; x++) {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for (int y = 0; y < map_height; y++) {
                int tile_id = GetIdUsingPerlin(x, y);
                noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y);
            }
        }
    }

    /* Using the perlin function, output a id that will set the tiles,
    it also sacales the Perlin value to the number of tiles avaiable */
    private int GetIdUsingPerlin(int x, int y) {
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * tileset.Count;
        if(scaled_perlin == 4) scaled_perlin = 3;
        return Mathf.FloorToInt(scaled_perlin);
    }

    //Creates the gameobject with the values given and it saves it in its group
    private void CreateTile(int tile_id, int x, int y) {
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);

        tile_grid[x].Add(tile);
    }
}
