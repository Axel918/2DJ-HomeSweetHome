using System.Collections;
using System.Collections.Generic;
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

    // TO BE REMOVED!!!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
            
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

        // Load All Necessary Scenes for Gameplay
        string[] scenes = { "GameScene", "GameUIScene", "Level" + PlayerManager.Instance.PlayerData.CurrentLevel };

        // Load the Scenes with Fade In Transition
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public IEnumerator GameOver()
    {
        SetGameState(GameState.LEVEL_FAILED);
        PlayerEvents.Instance.SetPlayerEnable(false);

        yield return null;

        RestartGame();
    }
}
