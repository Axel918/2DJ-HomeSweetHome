using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Drawing Drawing { get; private set; }                                // Drawing Class Reference
    public PlayerMovement PlayerMovement { get; private set; }                  // PlayerMovement Class Reference
    public PlayerSanity PlayerSanity { get; private set; }                      // PlayerSanity Class Reference
    public NavMeshAgent NavMeshAgent { get; private set; }                      // NavMeshAgent Component Reference

    void OnEnable()
    {
        PlayerManager.Instance.Player = this;
    }

    void OnDisable()
    {
        PlayerManager.Instance.Player = null;
    }

    void Awake()
    {
        // Cache-In Variables
        Drawing = GetComponent<Drawing>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerSanity = GetComponent<PlayerSanity>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
}