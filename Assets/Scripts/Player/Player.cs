using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Camera Shake Parameters")]
    [Range(0f, 1f)] [SerializeField] private float amplitude = 0.5f;
    [Range(0f, 1f)] [SerializeField] private float frequency = 0.15f;

    public PlayerMovement PlayerMovement { get; private set; }                                  // PlayerMovement Class Reference
    public PlayerSanity PlayerSanity { get; private set; }                                      // PlayerSanity Class Reference
    public NavMeshAgent NavMeshAgent { get; private set; }                                      // NavMeshAgent Component Reference

    [field: SerializeField, Header("References")]
    public CinemachineVirtualCamera PlayerCamera { get; private set; }                          // Player Camera Reference
    public CinemachineBasicMultiChannelPerlin Perlin { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }                      // Animator Component Reference
    [field : SerializeField] public Transform PlayerSpriteTransform { get; set; }               // Player Sprite Transform Reference

    void OnEnable()
    {
        PlayerManager.Instance.Player = this;
        PlayerEvents.Instance.OnSetPlayerEnable += SetCameraActive;
        PlayerEvents.Instance.OnPlayerInsane += SetCameraShake;
        PlayerEvents.Instance.OnPlayerStabilized += SetCameraShake;
        GameEvents.Instance.OnLevelFailed += SetCameraShake;
    }

    void OnDisable()
    {
        PlayerManager.Instance.Player = null;
        PlayerEvents.Instance.OnSetPlayerEnable -= SetCameraActive;
        PlayerEvents.Instance.OnPlayerInsane -= SetCameraShake;
        PlayerEvents.Instance.OnPlayerStabilized -= SetCameraShake;
        GameEvents.Instance.OnLevelFailed -= SetCameraShake;
    }

    void Awake()
    {
        // Cache-In Variables
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerSanity = GetComponent<PlayerSanity>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Perlin = PlayerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        SetCameraActive(true);
    }

    #region Player Camera Events
    /// <summary>
    /// Activated/Deactivates the Player Camera
    /// </summary>
    /// <param name="value"></param>
    void SetCameraActive(bool value)
    {
        PlayerCamera.gameObject.SetActive(value);
    }

    void SetCameraShake()
    {
        // Create Amplitude and Frequency Variable
        float amp;
        float freq;

        // Assign Values of Amplitude and Frequency based on Current Game State
        if (GameManager.Instance.State == GameManager.GameState.MONSTER_PRESENT)
        {
            amp = amplitude;
            freq = frequency;
        }
        else
        {
            amp = 0f;
            freq = 0f;
        }

        // Set Valujes of Amp and Freq
        Perlin.m_AmplitudeGain = amp;
        Perlin.m_FrequencyGain = freq;
    }
    #endregion
}