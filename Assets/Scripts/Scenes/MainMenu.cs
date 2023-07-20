using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.PlayerData.ClearData();

        PanelManager.Instance.ActivatePanel("Main Menu");
    }

    public void OnPlayButtonClicked()
    {
        string[] scenes = { "GameScene", "GameUIScene", "Level" + PlayerManager.Instance.PlayerData.CurrentLevel };

        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnCreditsButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("Credits Menu");
    }

    public void OnGameExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }

    public void OnReturnButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("Main Menu");
    }
}
