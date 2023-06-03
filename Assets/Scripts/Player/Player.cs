using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Drawing Drawing { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerSanity PlayerSanity { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

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
