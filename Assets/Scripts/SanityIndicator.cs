using UnityEngine;

public class SanityIndicator : MonoBehaviour
{
    [Header("Properties")]
    [Range(0, 5)] [SerializeField] private int minSanityLevel;                  // Minimum Sanity Level
    [Range(0, 5)] [SerializeField] private int maxSanityLevel;                  // Maximum Sanity Level

    private int currentSanityLevel = 0;
    private Animator animator;                                                  // Animator Component Reference

    void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerSanity += SetVignetteIntensity;
        PlayerEvents.Instance.OnPlayerStabilized += ResetVignette;
        PlayerEvents.Instance.OnPlayerInsane += Maximize;
        GameEvents.Instance.OnLevelFailed += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerSanity -= SetVignetteIntensity;
        PlayerEvents.Instance.OnPlayerStabilized -= ResetVignette;
        PlayerEvents.Instance.OnPlayerInsane -= Maximize;
        GameEvents.Instance.OnLevelFailed -= Terminate;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentSanityLevel = minSanityLevel;
        animator.SetInteger("sanityLevel", currentSanityLevel);

        Debug.Log("Sanity Level: " + currentSanityLevel);

        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Heartbeat lvl " + currentSanityLevel);
    }

    /// <summary>
    /// Sets the Intensity Value of the Vignette
    /// </summary>
    /// <param name="value"></param>
    void SetVignetteIntensity(int value)
    {
        if (currentSanityLevel > 0)
            AudioManager.Instance.Stop("Heartbeat lvl " + currentSanityLevel);

        // Increment Current Sanity Level
        currentSanityLevel++;
        
        // Play Heart Beat SFX if Current Sanity Level is Greater than 0
        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Heartbeat lvl " + currentSanityLevel);
        
        animator.SetInteger("sanityLevel", currentSanityLevel);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    /// <summary>
    /// Reset Vignette Upon Player Stabilization
    /// </summary>
    void ResetVignette()
    {
        // Stop Currently Playing Heartbeat SFX
        AudioManager.Instance.Stop("Heartbeat lvl " + currentSanityLevel);
        
        currentSanityLevel = minSanityLevel;

        // Play New HeartBeat SFX
        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Heartbeat lvl " + currentSanityLevel);

        animator.SetInteger("sanityLevel", currentSanityLevel);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    void Maximize()
    {
        if (currentSanityLevel > 0)
            // Stop Currently Playing Heartbeat SFX
            AudioManager.Instance.Stop("Heartbeat lvl " + currentSanityLevel);

        currentSanityLevel = maxSanityLevel;

        AudioManager.Instance.Play("Heartbeat lvl " + currentSanityLevel);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
        animator.SetInteger("sanityLevel", currentSanityLevel);
    }

    void Terminate()
    {
        AudioManager.Instance.Stop("Heartbeat lvl " + currentSanityLevel);
        currentSanityLevel = 0;
        animator.SetInteger("sanityLevel", currentSanityLevel);
    }
}
