using UnityEngine;
using UnityEngine.Video;

public class GameEndingController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VideoPlayer videoPlayer;                                   // Video Player Component Reference
    
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("Cutscene");

        // Subscribe EndGame Function to Video Player End Event
        videoPlayer.loopPointReached += EndGame;
    }

    /// <summary>
    /// Declares the Game as Finished. Return to Main Menu
    /// </summary>
    void EndGame(VideoPlayer vp)
    {
        // Indicate Game is Finished
        PlayerManager.Instance.PlayerData.GameIsFinished = true;

        // Load the Scenes
        string[] scenes = { "MainMenuScene" };

        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }
}