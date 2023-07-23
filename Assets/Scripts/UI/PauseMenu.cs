using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject pausePanel;

    void Awake()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
            return;

        if (GameIsPaused)
        {
            if (!pausePanel.activeSelf)
                return;

            if (Input.GetKeyDown(KeyCode.M))
            {
                OpenMainMenu();
                return;
            }
        }

        // Escape can Resume and Pause the Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    private void OpenMainMenu()
    {
        Time.timeScale = 1f;

        string[] scenes = { "MainMenuScene" };
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
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
