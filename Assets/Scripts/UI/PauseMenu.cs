using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused { get; private set; }

    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameIsPaused)
        {
            if (!pausePanel.activeSelf) return;

            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("Going to Main Menu");
                return;
            }
        }


        // Escape can Resume and Pause the Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        Debug.Log("Resume");
        GameIsPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        Debug.Log("Pause");
        GameIsPaused = true;
    }
}
