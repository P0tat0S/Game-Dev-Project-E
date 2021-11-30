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
    public GameObject tree;
    public GameObject stone;
    private float enemySpawnTime;
    private float resourceSpawnTime;
    public float ResourceLife;
    private int numberOfDays;
    //Enemy drop
    public GameObject item;

    /**************
        Counters
    **************/
    private int score = 0;
    public Text timerText;
    public Text scoreText;
    private Renderer tempBackgroundColour;
    public string currentScene;

    private void Start() {
        //Get player gameobject and Script
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        //Get tempBackground renderer
        tempBackgroundColour = GameObject.Find("temp_Background").GetComponent<Renderer>();

        EnemySpawnTimer();//Start Spawning enemies
        UpdateScore(0);
    }

    private void Update() {//Day system and enemy Spawn
        if (Time.timeSinceLevelLoad >= enemySpawnTime) {
            SpawnEnemy();
            EnemySpawnTimer();
        }
        if (Time.timeSinceLevelLoad >= resourceSpawnTime)
        {
            SpawnResource();
            ResourceSpawnTimer();
        }
        if (Input.GetKeyDown(KeyCode.R)) Restart();//R to restart
        DayTimer();
    }

    private void Update() {//Inputs
        if (Input.GetKeyDown(KeyCode.R)) Restart();//R to restart
    }

    //Function to Spawn enemies between 3 to 6 seconds and up to 1 and 3 seconds
    private void EnemySpawnTimer() {
        float growth = 1.0f - (2.0f/3.0f)*((numberOfDays+1.0f)/11.0f);// From 0 to 2/3 being the max
        float delay = Random.Range(3.0f*growth,6.0f*growth);
        enemySpawnTime = Time.timeSinceLevelLoad + delay;//Reset Timer
    }

    private void ResourceSpawnTimer(){
        float delay = Random.Range(5,15);
        resourceSpawnTime = Time.timeSinceLevelLoad + delay;
    }

    //Function to keep update numberof days, 1 day = 30 seconds
    private void DayTimer() {
        player.GetComponent<Player>().hungerSystem.Starve(0.01f);
        //Sin function that goes from 0 to 1 that will be a multiplier of the colour background
        float dayLight = 0.5f +  0.5f*(Mathf.Sin(((Time.timeSinceLevelLoad % 30.0f) / 30.0f) * 2f*Mathf.PI));
        numberOfDays = (int)(Time.timeSinceLevelLoad / 30);
        timerText.text = "Day: " + numberOfDays.ToString();
        tempBackgroundColour.material.color = new Color(1*dayLight,1*dayLight,1*dayLight,1);//dayTime system
    }

    private void SpawnEnemy() {
        //Vector for random position in the map
        Vector3 position = new Vector3(Random.Range(-19.0f, 19.0f), Random.Range(-11.0f,11.0f), 0.0f);
        if(Random.Range(0.0f,1.0f) > 0.33f) {//Spawn Knight with 66% chance
            Instantiate(knight, position, Quaternion.identity);
        } else {//Spawn Archer with 33% chance
            Instantiate(archer, position, Quaternion.identity);
        }
    }

    private void SpawnResource()
    {
        //Vector for random position in the map
        Vector3 position = new Vector3(Random.Range(-19.0f, 19.0f), Random.Range(-11.0f, 11.0f), 0.0f);
        if (Random.Range(0.0f, 1.0f) > 0.33f){//Spawn tree with 66% chance
            Instantiate(tree, position, Quaternion.identity);
        }
        else{//Spawn stone with 33% chance
            Instantiate(stone, position, Quaternion.identity);
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

    public void dropItem(Transform position) {
        //chance to drop item
        if(Random.value > 0.7) {        
            Instantiate(item, position);
        }
    }
}
