using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.PlayerData.ClearData();

        AudioManager.Instance.Play("Main Menu Music");

        // Activate Panel based on Game Status
        if (PlayerManager.Instance.PlayerData.GameIsFinished)
        {
            // Roll Credits if Player Has Finished the Game
            PanelManager.Instance.ActivatePanel("Credits Menu");
        }  
        else
        {
            // Activate Main Menu Panel
            PanelManager.Instance.ActivatePanel("Main Menu");
        }
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.Play("Button Sound");
        
        string[] scenes = { "LetterScene" };

        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnCreditsButtonClicked()
    {
        AudioManager.Instance.Play("Button Sound");
        PanelManager.Instance.ActivatePanel("Credits Menu");
    }

    public void OnGameExitButtonClicked()
    {
        AudioManager.Instance.Play("Button Sound");

        Debug.Log("You have Quit the Game.");
        Application.Quit();
    }

    public void OnReturnButtonClicked()
    {
        AudioManager.Instance.Play("Button Sound");
        PanelManager.Instance.ActivatePanel("Main Menu");
        PlayerManager.Instance.PlayerData.GameIsFinished = false;
    }
}