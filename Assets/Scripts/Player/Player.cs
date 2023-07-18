using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement { get; private set; }                  // PlayerMovement Class Reference
    public PlayerSanity PlayerSanity { get; private set; }                      // PlayerSanity Class Reference
    public NavMeshAgent NavMeshAgent { get; private set; }                      // NavMeshAgent Component Reference
    public Rigidbody Rb { get; private set; }                                   // RigidBody Component Reference

    [Header("References")]
    public CinemachineVirtualCamera PlayerCamera;                               // Player Camera Reference
    public Animator Animator;                                                   // Animator Component Reference

    void OnEnable()
    {
        PlayerManager.Instance.Player = this;
        PlayerEvents.Instance.OnSetPlayerEnable += SetCameraActive;
    }

    void OnDisable()
    {
        PlayerManager.Instance.Player = null;
        PlayerEvents.Instance.OnSetPlayerEnable -= SetCameraActive;
    }

    void Awake()
    {
        // Cache-In Variables
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerSanity = GetComponent<PlayerSanity>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
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