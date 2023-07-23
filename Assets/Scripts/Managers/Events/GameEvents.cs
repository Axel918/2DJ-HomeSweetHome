using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public event Action<bool> OnPauseSpawnTimer;                                     // Pauses the SpawnTimer Upon Transitioning from One
                                                                                     // Room to the Other

    public event Action OnLevelComplete;                                             // Gets Called when Player Completes all Tasks
    public event Action OnLevelFailed;                                               // Gets Called When Player Gets Caught by the Monster

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public void PauseSpawnTimer(bool value)
    {
        OnPauseSpawnTimer?.Invoke(value);
    }

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
}
