using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    private Material material;

    [Header("Intensity Levels")]
    [Range(0, 5)]
    [SerializeField] private int minSanityLevel;

    [Range(0, 5)]
    [SerializeField] private int maxSanityLevel;

    [Header("Static Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float maxStaticIntensity = 0.4f;

    #region "Private Properties"

    private float staticIncrement = 0f;
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
        staticIncrement = maxStaticIntensity / maxSanityLevel;

        // Set the current sanity level based on the Minimum Sanity Level
        currentSanityLevel = minSanityLevel;

        // Increase the CurrentStaticIntensity based on the player's insanity level
        for (int i = 0; i < currentSanityLevel; i++)
        {
            currentStaticIntensity += staticIncrement;
        }

        material.SetFloat("_Opacity", currentStaticIntensity);

        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Static lvl " + currentSanityLevel);
    }

    void IncreaseStatic(int value)
    {
        if (currentSanityLevel > 0)
            AudioManager.Instance.Stop("Static lvl " + currentSanityLevel);
        
        // Incremenent the Sanity Level by 1
        if (currentSanityLevel < maxSanityLevel)
        {
            // Increase the CurrentStaticIntensity by the staticIncrement; 
            currentStaticIntensity += staticIncrement;
            material.SetFloat("_Opacity", currentStaticIntensity);

            currentSanityLevel++;
        }

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);

        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Static lvl " + currentSanityLevel);
    }

    void ResetStatic()
    {
        AudioManager.Instance.Stop("Static lvl " + currentSanityLevel);

        // Sets the currentSanityLevel back to the Minimum Level
        currentSanityLevel = minSanityLevel;

        currentStaticIntensity = 0f;

        // Increase the CurrentStaticIntensity based on the player's insanity level
        for (int i = 0; i < currentSanityLevel; i++)
        {
            currentStaticIntensity += staticIncrement;
        }

        material.SetFloat("_Opacity", currentStaticIntensity);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);

        if (currentSanityLevel > 0)
            AudioManager.Instance.Play("Static lvl " + currentSanityLevel);
    }

    void Maximize()
    {
        if (currentSanityLevel > 0)
            AudioManager.Instance.Stop("Static lvl " + currentSanityLevel);

        currentSanityLevel = maxSanityLevel;

        AudioManager.Instance.Play("Static lvl " + currentSanityLevel);

        currentStaticIntensity = maxStaticIntensity;

        material.SetFloat("_Opacity", currentStaticIntensity);

        // Clamps Sanity Level to Min-Max Values
        currentSanityLevel = Mathf.Clamp(currentSanityLevel, minSanityLevel, maxSanityLevel);
    }

    void Terminate()
    {
        AudioManager.Instance.Stop("Static lvl " + currentSanityLevel);
        currentSanityLevel = 0;
        currentStaticIntensity = 0f;
        material.SetFloat("_Opacity", currentStaticIntensity);
    }
}