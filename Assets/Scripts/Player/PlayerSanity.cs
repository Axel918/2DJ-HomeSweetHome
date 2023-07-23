using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [Range(0, 5)][SerializeField] private int minSanityLevel;                  // Minimum Sanity Level
    [Range(0, 5)][SerializeField] private int maxSanityLevel;                  // Maximum Sanity Level

    private float currentSanityLevel;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += () => AudioManager.Instance.Play("Flatline");
        PlayerEvents.Instance.OnPlayerStabilized += () => AudioManager.Instance.Stop("Flatline");
        GameEvents.Instance.OnLevelFailed += () => AudioManager.Instance.Stop("Flatline");
        PlayerEvents.Instance.OnPlayerStabilized += ResetSanity;
        PlayerEvents.Instance.OnSetPlayerSanity += DecreaseSanity;

    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= () => AudioManager.Instance.Play("Flatline");
        PlayerEvents.Instance.OnPlayerStabilized -= () => AudioManager.Instance.Stop("Flatline");
        GameEvents.Instance.OnLevelFailed -= () => AudioManager.Instance.Stop("Flatline");
        PlayerEvents.Instance.OnPlayerStabilized -= ResetSanity;
        PlayerEvents.Instance.OnSetPlayerSanity -= DecreaseSanity;
    }

    void Start()
    {
        currentSanityLevel = minSanityLevel;
    }

    /// <summary>
    /// Reset Sanity Values
    /// </summary>
    void ResetSanity()
    {
        currentSanityLevel = minSanityLevel;

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    /// <summary>
    /// Decreases Player's Sanity Based on Given Amount
    /// </summary>
    /// <param name="amount"></param>
    void DecreaseSanity(int amount)
    {
        currentSanityLevel += amount;

        if (currentSanityLevel >= maxSanityLevel)
        {
            // Clamps Sanity Level to Min-Max Values
            currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);

            OnInsane();
        }
    }

    /// <summary>
    /// Gets Triggered when Player Reaches Max Sanity
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
        return (float)currentSanityLevel / (float)maxSanityLevel;
    }
}