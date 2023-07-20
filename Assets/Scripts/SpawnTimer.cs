using System.Collections;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float monsterTimer = 10f;                                  // Countdown for Monster Spawning
    [SerializeField] private float insanityTimer = 10f;                                 // Countdown for When Player is Insane
    [Range(1, 100)] [SerializeField] private int scareProbability = 20;                 // Probability for Monster Scare
    [SerializeField] private string[] scareIds;                                         // Scare ID Array

    private bool isTransitioning;                                                       // Indicates if Player is Transitioning from One Room
                                                                                        // to Another

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
            MonsterScare();
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
                currentTimer--;
        }

        // Monster Gets Player
        // GAME OVER
        Debug.Log("GAME OVER!!! YOU GOT CAUGHT BY THE MONSTER");
        GameManager.Instance.SetGameState(GameManager.GameState.LEVEL_FAILED);
        GameEvents.Instance.LevelFailed();
    }
    #endregion

    /// <summary>
    /// IsTransitioning Boolean Setter
    /// </summary>
    /// <param name="value"></param>
    void SetIsTransitioning(bool value)
    {
        isTransitioning = value;
    }

    /// <summary>
    /// Provides a Subtle Jumpscare for the Player
    /// </summary>
    void MonsterScare()
    {
        // Random Number Generation
        int randomNumberProbability = Random.Range(1, 101);
        int randomNumberScareIndex = Random.Range(0, scareIds.Length);

        Debug.Log("Monster Scare Probability: " + randomNumberProbability);

        if (scareProbability > randomNumberProbability)
            GameUIController.Instance.SetAnimation(scareIds[randomNumberScareIndex]);
    }
}