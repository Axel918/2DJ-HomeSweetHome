using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement { get; private set; }                  // PlayerMovement Class Reference
    public PlayerSanity PlayerSanity { get; private set; }                      // PlayerSanity Class Reference
    public NavMeshAgent NavMeshAgent { get; private set; }                      // NavMeshAgent Component Reference
    
    [Header("References")]
    public CinemachineVirtualCamera PlayerCamera;                               // Player Camera Reference

    void OnEnable()
    {
        PlayerManager.Instance.Player = this;
        PlayerEvents.Instance.OnSetPlayerMovement += SetCameraActive;
    }

    void OnDisable()
    {
        PlayerManager.Instance.Player = null;
        PlayerEvents.Instance.OnSetPlayerMovement -= SetCameraActive;
    }

    void Awake()
    {
        // Cache-In Variables
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerSanity = GetComponent<PlayerSanity>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        SetCameraActive(true);
    }

    /// <summary>
    /// Activated/Deactivates the Player Camera
    /// </summary>
    /// <param name="value"></param>
    void SetCameraActive(bool value)
    {
        PlayerCamera.gameObject.SetActive(value);
    }
}