using System.Collections;
using UnityEngine;

/// <summary>
/// Provides a Counter for Spawning Chances
/// </summary>
public class MonsterSpawn : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float monsterTimer = 10f;                                  // Countdown for Monster Spawning

    private Monster monsterSetup;                                                       // Monster Class Reference

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerStabilized += InitiateMonsterCountdown;
        PlayerEvents.Instance.OnPlayerInsane += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerStabilized -= InitiateMonsterCountdown;
        PlayerEvents.Instance.OnPlayerInsane -= Terminate;
    }

    void Awake()
    {
        monsterSetup = GetComponent<Monster>();
        InitiateMonsterCountdown();
    }

    /// <summary>
    /// Start Countdown for Monster Spawning
    /// </summary>
    void InitiateMonsterCountdown()
    {
        // Start the Monster Countdown
        StartCoroutine(MonsterTimer());
    }

    IEnumerator MonsterTimer()
    {
        float currentTimer = monsterTimer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);

            if (!monsterSetup.IsTransitioning)
                currentTimer--;
        }

        CheckMonster();
    }

    /// <summary>
    /// Checks if the Monster Will Spawn
    /// </summary>
    void CheckMonster()
    {
        // Random Number Generation
        int randomNumber = Random.Range(1, 101);

        // Get the Probability Based on Player's Current Sanity
        float probability = (1f - PlayerManager.Instance.Player.PlayerSanity.GetSanityRatio()) * 100f;

        Debug.Log("Probability: " + probability + ", Random Number: " + randomNumber);

        if (probability > randomNumber)
        {
            Debug.Log("There's A Monster!");
            GameManager.Instance.SetGameState(GameManager.GameState.MONSTER_PRESENT);
            PlayerEvents.Instance.PlayerInsane();
        }
        else
        {
            Debug.Log("You're Safe!");
            StartCoroutine(MonsterTimer());
        }
    }

    /// <summary>
    /// Stops the Timer
    /// </summary>
    void Terminate()
    {
        StopAllCoroutines();
    }
}