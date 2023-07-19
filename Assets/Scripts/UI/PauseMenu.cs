using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (GameIsPaused)
        {
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

        PanelManager.Instance.ActivatePanel("Game UI");
        Debug.Log("Resume");
        GameIsPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;

        PanelManager.Instance.ActivatePanel("Pause Menu");
        Debug.Log("Pause");
        GameIsPaused = true;
    }
}
