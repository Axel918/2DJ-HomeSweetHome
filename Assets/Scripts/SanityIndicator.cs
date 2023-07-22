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
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerSanity -= SetVignetteIntensity;
        PlayerEvents.Instance.OnPlayerStabilized -= ResetVignette;
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
    }

    /// <summary>
    /// Sets the Intensity Value of the Vignette
    /// </summary>
    /// <param name="value"></param>
    void SetVignetteIntensity(int value)
    {
        //if (currentSanityLevel > 0)
            //AudioManager.Instance.Stop("Heatbeat Final");

        // Increment Current Sanity Level
        currentSanityLevel++;
        

        // Play Heart Beat SFX if Current Sanity Level is Greater than 0
        //if (currentSanityLevel > 0)
            // AudioManager.Instance.Play("Heatbeat Final");
        
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
        //AudioManager.Instance.Stop("Heatbeat Final");
        
        currentSanityLevel = minSanityLevel;

        // Play New HeartBeat SFX
        //if (currentSanityLevel > 0)
            // AudioManager.Instance.Play("Heatbeat Final");

        animator.SetInteger("sanityLevel", currentSanityLevel);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }
}
