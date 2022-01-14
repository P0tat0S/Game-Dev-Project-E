using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    /***********
        Player
    ***********/
    private GameObject player;
    private Player playerScript;
    public GameObject gameOverScreen;

    /*******************
        Enemy Spawning
    *******************/
    public GameObject knight;
    public GameObject archer;
    public bool enableEnemySpawn;
    private float enemySpawnTime;
    private float resourceSpawnTime;
    private int numberOfDays;
    private float enemyGrowth;
    public float EnemeyGrowth => enemyGrowth;

    /***********************
        Resource Spawning
    ***********************/
    private GameObject Resources;
    public GameObject tree;
    public GameObject stone;
    //Add more Items
    public GameObject berry;

    /**************
        Counters
    **************/
    private int score = 0;
    public Text timerText;
    public Text scoreText;
    private Renderer backgroundColor;

    /**********
        Tiles
    **********/
    public Renderer tile_water;

    //Reload
    public string currentScene;
    private void Start() {
        //Get player gameobject and Script
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        //Get renderer of a tile prefabs
        backgroundColor = tile_water;

        //Just to tidy Object Hierarchy
        Resources = GameObject.Find("Resources");

        enemyGrowth = 0f;
        EnemySpawnTimer();//Start Spawning enemies
        UpdateScore(0);
    }

    private void Update() {//Day system and enemy Spawn
        if (Time.timeSinceLevelLoad >= enemySpawnTime && enableEnemySpawn) {
            var enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemiesSpawned.Length < 100) SpawnEnemy();//Max 100 enemies
            EnemySpawnTimer();
        }
        if (Time.timeSinceLevelLoad >= resourceSpawnTime) {
            var resourcesSpawned = GameObject.FindGameObjectsWithTag("ResourcePoint");
            if (resourcesSpawned.Length < 100) SpawnResource();//Max 100 resource points
            ResourceSpawnTimer();
        }
        if (Input.GetKeyDown(KeyCode.R)) Restart();//R to restart
        DayTimer();
    }

    //Function to Spawn enemies between 3 to 6 seconds and up to 1 and 3 seconds
    private void EnemySpawnTimer() {
        float timerGrowth = 1.0f - (2.0f/3.0f)*((numberOfDays+1.0f)/21.0f);// From 0 to 2/3 being the max
        float delay = Random.Range(6.0f*timerGrowth,12.0f*timerGrowth);
        enemySpawnTime = Time.timeSinceLevelLoad + delay;//Reset Timer
    }

    private void ResourceSpawnTimer(){
        float delay = Random.Range(5,15);
        resourceSpawnTime = Time.timeSinceLevelLoad + delay;
    }

    //Function to keep update numberof days, 1 day = 30 seconds
    private void DayTimer() {
        player.GetComponent<Player>().hungerSystem.Starve(0.001f);
        //Sin function that goes from 0 to 1 that will be a multiplier of the colour background
        float dayLight = 0.5f +  0.5f*(Mathf.Sin(((Time.timeSinceLevelLoad % 30.0f) / 30.0f) * 2f*Mathf.PI));
        numberOfDays = (int)(Time.timeSinceLevelLoad / 30);
        enemyGrowth = 2f * numberOfDays;// 2 4 6 8 10 12
        timerText.text = "Day: " + numberOfDays.ToString();
        float clampedDayLight = Mathf.Clamp(dayLight, 0.5f, 1.0f);
        backgroundColor.sharedMaterial.color = new Color(clampedDayLight, clampedDayLight, clampedDayLight, 1);//dayTime system
    }

    private void SpawnEnemy() {
        //Vector for random position in the map
        Vector3 position = new Vector3(Random.Range(-45.0f, 45.0f), Random.Range(-140.0f,140.0f), 0.0f);
        if(Random.Range(0.0f,1.0f) > 0.33f) {//Spawn Knight with 66% chance
            Instantiate(knight, position, Quaternion.identity);
        } else {//Spawn Archer with 33% chance
            Instantiate(archer, position, Quaternion.identity);
        }
    }

    private void SpawnResource() {
        //Vector for random position in the map
        for (int i = 0; i < 10; i++) {
            Vector3 position = new Vector3(Random.Range(-45.0f, 45.0f), Random.Range(-140.0f, 140.0f), 0.0f);
            float chance = Random.Range(0.0f, 1.0f);
            if ( chance < 0.33f) {//Spawn tree with 33% chance
                Instantiate(tree, position, Quaternion.identity, Resources.transform);
            } else if (chance < 0.66f) {//Spawn stone with 33% chance
                Instantiate(stone, position, Quaternion.identity, Resources.transform);
            } else { //Spawn Berry with 33% chance 
                Instantiate(berry, position, Quaternion.identity, Resources.transform);
            }
        }
        
    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowGameOver() {
        Transform canvas = GameObject.Find("Canvas").transform;
        Instantiate(gameOverScreen, canvas);
    }

    public void Restart() {
        SceneManager.LoadScene(currentScene);
    }
}
