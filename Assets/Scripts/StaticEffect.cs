using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    private Material material;

    [Header("Intensity Levels")]
    [Range(0, 5)]
    [SerializeField] private int minSanityLevel;

    [Range(0, 5)]
    [SerializeField] private int maxSanityLevel;

    #region "Private Properties"
    private int currentSanityLevel = 0;
    private float currentStaticIntensity = 0f;
    #endregion

    void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerSanity += IncreaseStatic;
        PlayerEvents.Instance.OnPlayerStabilized += ResetStatic;
        PlayerEvents.Instance.OnPlayerInsane += Maximize;
        GameEvents.Instance.OnLevelFailed += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerSanity -= IncreaseStatic;
        PlayerEvents.Instance.OnPlayerStabilized -= ResetStatic;
        PlayerEvents.Instance.OnPlayerInsane -= Maximize;
        GameEvents.Instance.OnLevelFailed -= Terminate;
    }

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        // Set the current sanity level based on the Minimum Sanity Level
        currentSanityLevel = minSanityLevel;

        // Increase the CurrentStaticIntensity based on the player's insanity level
        for (int i = 0; i < currentSanityLevel; i++)
        {
            currentStaticIntensity += 0.1f;
        }

        material.SetFloat("_Opacity", currentStaticIntensity);
    }

    void IncreaseStatic(int value)
    {
        // Incremenent the Sanity Level by 1
        if (currentSanityLevel < maxSanityLevel)
        {
            // Increase the CurrentStaticIntensity by 0.1f; 
            currentStaticIntensity += 0.1f;
            material.SetFloat("_Opacity", currentStaticIntensity);

            currentSanityLevel++;
        }

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    void ResetStatic()
    {
        // Sets the currentSanityLevel back to the Minimum Level
        currentSanityLevel = minSanityLevel;

        currentStaticIntensity = 0f;

        // Increase the CurrentStaticIntensity based on the player's insanity level
        for (int i = 0; i < currentSanityLevel; i++)
        {
            currentStaticIntensity += 0.1f;
        }

        material.SetFloat("_Opacity", currentStaticIntensity);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    void Maximize()
    {
        currentSanityLevel = maxSanityLevel;

        currentStaticIntensity = 0.5f;

        material.SetFloat("_Opacity", currentStaticIntensity);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    void Terminate()
    {
        currentSanityLevel = 0;
        currentStaticIntensity = 0f;
        material.SetFloat("_Opacity", currentStaticIntensity);
    }
}