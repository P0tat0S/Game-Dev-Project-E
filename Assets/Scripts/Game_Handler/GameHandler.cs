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
    private float enemySpawnTime;
    private int numberOfDays;

    /**************
        Counters
    **************/
    private int score = 0;
    public Text timerText;
    public Text scoreText;
    
    private void Start() {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        EnemySpawnTimer();//Start Spawning enemies
        UpdateScore(0);
    }

    private void FixedUpdate() {
        if (Time.timeSinceLevelLoad >= enemySpawnTime) {
            SpawnEnemy();
            EnemySpawnTimer();
        }
        DayTimer();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) Restart();//R to restart
    }
 
    //Function to Spawn enemies between 3 to 6 seconds and up to 1 and 3 seconds
    private void EnemySpawnTimer() {
        float growth = 1.0f - (2.0f/3.0f)*((numberOfDays+1.0f)/11.0f);
        float delay = Random.Range(3.0f*growth,6.0f*growth);
        enemySpawnTime = Time.timeSinceLevelLoad + delay;//Reset Timer
    }

    //Function to keep update numberof days, 1day = 30seconds
    private void DayTimer() {
        numberOfDays = (int)(Time.timeSinceLevelLoad/30);
        timerText.text = "Day: " + numberOfDays.ToString();
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

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowGameOver() {
        Transform canvas = GameObject.Find("Canvas").transform;
        Instantiate(gameOverScreen, canvas);
    }

    public void Restart() {
        SceneManager.LoadScene("main");
    }
}
