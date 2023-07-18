using UnityEngine;

public class SanityIndicator : MonoBehaviour
{
    private Animator animator;                                  // Animator Component Reference

    private int sanityLevel = 0;

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
        sanityLevel = 0;

        animator.SetInteger("sanityLevel", sanityLevel);
    }
}
