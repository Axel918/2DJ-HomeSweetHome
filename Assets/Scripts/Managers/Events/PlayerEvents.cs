using UnityEngine;
using System;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents Instance;

    public event Action<float> OnPlayerDamaged;                                             // Called When Player Takes Damage
    public event Action<bool> OnSetPlayerMovement;                                          // Activates/Deactivates Player Movement
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

    public void PlayerDamaged(float value)
    {
        OnPlayerDamaged?.Invoke(value);
    }

    public void SetPlayerMovement(bool condition)
    {
        OnSetPlayerMovement?.Invoke(condition);
    }

    public void PlayerStabilized()
    {
        OnPlayerStabilized?.Invoke();
    }
}
