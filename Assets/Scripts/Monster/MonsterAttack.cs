using System.Collections;
using UnityEngine;

/// <summary>
/// Handles Time To Attack When Player Goes Insane
/// </summary>
public class MonsterAttack : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float insanityTimer = 10f;                                 // Countdown for When Player is Insane

    private Monster monsterSetup;                                                       // Monster Class Reference

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += InitiateInsanityTimer;
        PlayerEvents.Instance.OnPlayerStabilized += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= InitiateInsanityTimer;
        PlayerEvents.Instance.OnPlayerStabilized -= Terminate;
    }

    void Awake()
    {
        monsterSetup = GetComponent<Monster>();
    }

    void InitiateInsanityTimer()
    {
        StartCoroutine(InsanityTimer());
    }

    IEnumerator InsanityTimer()
    {
        float currentTimer = insanityTimer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);

            if (!monsterSetup.IsTransitioning)
                currentTimer--;
        }

        // Prevent Monster from Attacking the Player when Playing
        // the Soothing Mini-Game
        if (SoothingMiniGame.Instance.IsPlaying)
            yield break;

        // Monster Gets Player. GAME OVER!!!
        Debug.Log("GAME OVER!!! YOU GOT CAUGHT BY THE MONSTER");
        GameManager.Instance.SetGameState(GameManager.GameState.LEVEL_FAILED);
        GameEvents.Instance.LevelFailed();
    }

    /// <summary>
    /// Stops the Timer
    /// </summary>
    void Terminate()
    {
        StopAllCoroutines();
    }
}