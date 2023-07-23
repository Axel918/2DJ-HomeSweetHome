using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        NO_MONSTER,
        MONSTER_PRESENT,
        LEVEL_COMPLETE,
        LEVEL_FAILED
    }
    
    public static GameManager Instance;

    public GameState State { get; private set; } = GameState.NO_MONSTER;               // Current Game Status Indicator
    public bool PlayerIsSafe { get; set; }                                             // Indicates if Player is Safe from Monster Attacks

    [field : SerializeField, Header("Properties")] 
    public int MaxLevel { get; private set; } = 3;                                      // Maximum Level Amount

    // TO BE REMOVED!!!
    public int CurrentNumber { get; private set; }
    public int TotalFurniture { get; set; }

    void OnEnable()
    {
        GameEvents.Instance.OnLevelFailed += GameOver;
    }

    void OnDisable()
    {
        GameEvents.Instance.OnLevelFailed -= GameOver;
    }

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    /// <summary>
    /// Sets the Game Status
    /// </summary>
    /// <param name="value"></param>
    public void SetGameState(GameState value)
    {
        State = value;
    }

    /// <summary>
    /// Resets Entire Level
    /// </summary>
    public void RestartGame()
    {        
        // Check if Level is Complete
        // If so, Increment the Current Level
        if (State == GameState.LEVEL_COMPLETE)
            PlayerManager.Instance.PlayerData.CurrentLevel++;

        if (PlayerManager.Instance.PlayerData.CurrentLevel < MaxLevel + 1)
        {
            // Load Letter Scene
            string[] scenes = { "LetterScene" };

            // Load the Scenes with Fade In Transition
            SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
        }
        else
        {
            // Load Ending Scene
            string[] scenes = { "EndingScene" };

            // Load the Scenes with Fade In Transition
            SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
        }
    }

    /// <summary>
    /// TO BE REMOVED!!!
    /// </summary>
    public void CheckList()
    {
        CurrentNumber++;
        
        if (CurrentNumber >= TotalFurniture)
        {
            SetGameState(GameState.LEVEL_COMPLETE);
            RestartGame();
        }
    }

    void GameOver()
    {
        SetGameState(GameState.LEVEL_FAILED);
        PanelManager.Instance.ActivatePanel("");
    }
}
