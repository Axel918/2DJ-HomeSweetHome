using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float defaultSanity;

    private float currentSanity;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerDamaged += DecreaseSanity;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerDamaged -= DecreaseSanity;
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