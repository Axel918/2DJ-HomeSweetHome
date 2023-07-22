using UnityEngine;
using System;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents Instance;

    public event Action<int> OnSetPlayerSanity;                                           // Called When Player Takes Damage
    public event Action<bool> OnSetPlayerEnable;                                            // Activates/Deactivates Player Movement
    public event Action OnPlayerStabilized;                                                 // Called When Player Finishes Sanity Stabilization
    public event Action OnPlayerInsane;                                                     // Triggered when Player Reaches 0 Sanity

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
    public void SetPlayerSanity(int value)
    {
        OnSetPlayerSanity?.Invoke(value);
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

    /// <summary>
    /// Gets Called when Monster is Present (Player Loses Sanity)
    /// </summary>
    public void PlayerInsane()
    {
        OnPlayerInsane?.Invoke();
    }
}
