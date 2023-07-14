using UnityEngine;
using System;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents Instance;

    public event Action<float> OnPlayerDamaged;                                             // Called When Player Takes Damage
    public event Action<bool> OnSetPlayerEnable;                                          // Activates/Deactivates Player Movement
    public event Action OnPlayerStabilized;                                                 // Called When Player Finishes Sanity Stabilization

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
    /// Gets Called when Player Fails a Furniture Mini-Game
    /// </summary>
    /// <param name="value"></param>
    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
    }

    /// <summary>
    /// Enables/Disables Player Movement and Camera
    /// </summary>
    /// <param name="condition"></param>
    public void SetPlayerEnable(bool condition)
    {
        OnSetPlayerEnable?.Invoke(condition);
    }

    /// <summary>
    /// Gets Called when Player Successfully Finishes a Soothing Mini-Game
    /// </summary>
    public void PlayerStabilized()
    {
        OnPlayerStabilized?.Invoke();
    }
}
