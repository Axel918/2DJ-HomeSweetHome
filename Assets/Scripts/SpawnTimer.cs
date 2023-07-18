using System.Collections;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float monsterTimer = 10f;
    [SerializeField] private float insanityTimer = 10f;

    private bool isTransitioning;

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerStabilized += InitiateMonsterCountdown;
        PlayerEvents.Instance.OnPlayerInsane += InitiateInsanityTimer;
        GameEvents.Instance.OnPauseSpawnTimer += SetIsTransitioning;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerStabilized -= InitiateMonsterCountdown;
        PlayerEvents.Instance.OnPlayerInsane -= InitiateInsanityTimer;
        GameEvents.Instance.OnPauseSpawnTimer -= SetIsTransitioning;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitiateMonsterCountdown();
    }

    #region Monster Management
    /// <summary>
    /// Start Countdown for Monster Spawning
    /// </summary>
    void InitiateMonsterCountdown()
    {
        // Cancels Coroutines if There Are Any
        StopAllCoroutines();

        // Start the Monster Countdown
        StartCoroutine(MonsterTimer());
    }

    IEnumerator MonsterTimer()
    {
        float currentTimer = monsterTimer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);

            if (!isTransitioning)
            {
                currentTimer--;
            }
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

            // Add 40/60 Chance of Monster Scaring the Player
            // 40 - Scare
            // 60 - Nothing
        }
    }
    #endregion

    #region Player Insanity Management
    void InitiateInsanityTimer()
    {
        StopAllCoroutines();

        StartCoroutine(InsanityTimer());
    }

    IEnumerator InsanityTimer()
    {
        float currentTimer = insanityTimer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            

            if (!isTransitioning)
            {
                currentTimer--;
            }
        }

        // Monster Gets Player
        // GAME OVER
        StartCoroutine(GameManager.Instance.GameOver());
        Debug.Log("GAME OVER!!! YOU GOT CAUGHT BY THE MONSTER");
    }
    #endregion

    void SetIsTransitioning(bool value)
    {
        isTransitioning = value;
    }
}