using UnityEngine;

public class SanityIndicator : MonoBehaviour
{
    private Animator animator;                                  // Animator Component Reference

    private int sanityLevel;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerDamaged += SetVignetteIntensity;
        PlayerEvents.Instance.OnPlayerStabilized += ResetVignette;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerDamaged -= SetVignetteIntensity;
        PlayerEvents.Instance.OnPlayerStabilized -= ResetVignette;
    }

    void Awake()
    {
        sanityLevel = PlayerManager.Instance.PlayerData.CurrentLevel - 1;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets the Intensity Value of the Vignette
    /// </summary>
    /// <param name="value"></param>
    void SetVignetteIntensity(float value)
    {
        sanityLevel++;

        animator.SetInteger("sanityLevel", sanityLevel);
    }

    /// <summary>
    /// Reset Vignette Upon Player Stabilization
    /// </summary>
    void ResetVignette()
    {
        sanityLevel = PlayerManager.Instance.PlayerData.CurrentLevel - 1;
        animator.SetInteger("sanityLevel", sanityLevel);
    }
}
