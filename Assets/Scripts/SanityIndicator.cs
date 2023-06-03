using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class SanityIndicator : MonoBehaviour
{
    private PostProcessVolume volume;
    private Vignette vignette;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerDamaged += SetVignette;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerDamaged -= SetVignette;
    }

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    void SetVignette(float value)
    {
        StartCoroutine(SetDelay());
        
    }

    IEnumerator SetDelay()
    {
        yield return null;

        vignette.intensity.value = (1f - PlayerManager.Instance.Player.PlayerSanity.GetSanityRatio());
        Debug.Log(1f - PlayerManager.Instance.Player.PlayerSanity.GetSanityRatio());
    }
}
