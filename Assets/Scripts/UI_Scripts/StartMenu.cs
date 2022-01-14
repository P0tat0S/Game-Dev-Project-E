using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject startMenuUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) {
            Pause();
            paused = true;
        }
    }

    public void Resume() {
        startMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause() {
        startMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Quit() {
        Application.Quit();
    }
}
