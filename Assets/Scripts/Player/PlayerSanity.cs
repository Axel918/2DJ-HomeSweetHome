using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float defaultSanity;

    private float currentSanity;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerStabilized += Initialize;
        PlayerEvents.Instance.OnSetPlayerSanity += DecreaseSanity;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerStabilized -= Initialize;
        PlayerEvents.Instance.OnSetPlayerSanity -= DecreaseSanity;
    }

    void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize Values 
    /// </summary>
    void Initialize()
    {
        // Set Current Sanity Based on Default Sanity
        // Decrease Value of Default Sanity based on Current Level
        currentSanity = defaultSanity;
    }

    /// <summary>
    /// Decreases Player's Sanity Based on Given Amount
    /// </summary>
    /// <param name="amount"></param>
    void DecreaseSanity(float amount)
    {
        currentSanity -= amount;

        if (currentSanity <= 0f)
        {
            currentSanity = 0f;
            OnInsane();
        }
    }

    /// <summary>
    /// Gets Triggered when
    /// </summary>
    void OnInsane()
    {
        Debug.Log("Player is Insane");
        GameManager.Instance.SetGameState(GameManager.GameState.MONSTER_PRESENT);
        PlayerEvents.Instance.PlayerInsane();
    }

    /// <summary>
    /// Returns a Quotient of the Player's Current Sanity Level Over the Total/Default Sanity
    /// </summary>
    /// <returns></returns>
    public float GetSanityRatio()
    {
        return currentSanity / defaultSanity;
    }
}