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
        string[] scenes = { "LetterScene" };

        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnCreditsButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("Credits Menu");
    }

    public void OnGameExitButtonClicked()
    {
        Debug.Log("You have Quit the Game.");
        Application.Quit();
    }

    public void OnReturnButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("Main Menu");
    }
}