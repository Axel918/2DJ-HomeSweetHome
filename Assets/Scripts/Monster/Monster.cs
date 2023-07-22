using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterSpawn MonsterSpawn { get; private set; }                          // MonsterSpawn Class Reference
    public MonsterJumpscare MonsterJumpscare { get; private set; }                  // MonsterJumpScare Class Reference
    public MonsterAttack MonsterAttack { get; private set; }                        // MonsterAttack Class Reference
    public bool IsTransitioning { get; private set; }                               // Indicates if Player is Transitioning from One Room to Another

    void OnEnable()
    {
        GameEvents.Instance.OnPauseSpawnTimer += SetIsTransitioning;
    }

    void OnDisable()
    {
        GameEvents.Instance.OnPauseSpawnTimer -= SetIsTransitioning;
    }

    void Awake()
    {
        // Cache-In Variables
        MonsterSpawn = GetComponent<MonsterSpawn>();
        MonsterJumpscare = GetComponent<MonsterJumpscare>();
        MonsterAttack = GetComponent<MonsterAttack>();
    }

    /// <summary>
    /// IsTransitioning Boolean Setter
    /// </summary>
    /// <param name="value"></param>
    void SetIsTransitioning(bool value)
    {
        IsTransitioning = value;
    }
}