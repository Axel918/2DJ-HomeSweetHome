using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using DG.Tweening;

public class SanityIndicator : MonoBehaviour
{
    [SerializeField] private float easeDuration;                        // Duration of the Float Easing
    
    private PostProcessVolume volume;                                   // Post-Process Volume Component Reference
    private Vignette vignette;                                          // Vignette Refernce

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerDamaged += SetVignetteIntensity;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerDamaged -= SetVignetteIntensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    /// <summary>
    /// Sets the Intensity Value of the Vignette
    /// </summary>
    /// <param name="value"></param>
    void SetVignetteIntensity(float value)
    {
        // Don't Execute If Vignette Intensity Has Reached Max Value
        if (vignette.intensity.value.Equals(1f))
            return;
        
        StartCoroutine(EaseValue());
    }

    /// <summary>
    /// Set Float Value Gradually
    /// </summary>
    /// <returns></returns>
    IEnumerator EaseValue()
    {
        yield return new WaitForSeconds(0.01f);

        float endValue = 1f - PlayerManager.Instance.Player.PlayerSanity.GetSanityRatio();
        
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, endValue, 1f);
        Debug.Log("Set Vignette");
    }
}
