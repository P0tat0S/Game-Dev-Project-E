using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public static bool dead = false;
    public GameObject endMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) {
            
            Pause();
            dead = false;
        }
    }

    public void Resume() {
        endMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause() {
        endMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart() {
        // Get the current scene name
        string scene = SceneManager.GetActiveScene().name;

        // Load the scene
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
    }

    public void IsDead() {
        dead = true;
    }
}
