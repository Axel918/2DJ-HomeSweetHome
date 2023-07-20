using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public event Action<bool> OnPauseSpawnTimer;                                     // Pauses the SpawnTimer Upon Transitioning from One
                                                                                     // Room to the Other

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
}
