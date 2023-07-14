using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        NO_MONSTER,
        MONSTER_PRESENT
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
            
    }

    public void RestartGame()
    {
        string[] scenes = { "GameScene", "GameUIScene", "Level" + PlayerManager.Instance.PlayerData.CurrentLevel };

        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }
}
