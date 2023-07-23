using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Player Player { get; set; }                   // Player Game Object Reference
    public PlayerData PlayerData;                        // Player Data Class Reference

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
}