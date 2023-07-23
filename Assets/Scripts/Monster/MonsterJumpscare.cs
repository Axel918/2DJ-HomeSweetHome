using System.Collections;
using UnityEngine;

/// <summary>
/// Serves as a Countdown for Giving a Subtle Jumpscare to the Player
/// </summary>
public class MonsterJumpscare : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float jumpscareTimer = 10f;                                // Countdown for the Subtle Jumpscare
    [Range(1, 100)][SerializeField] private int scareProbability = 20;                  // Probability for Monster Scare
    [SerializeField] private string[] scareIds;                                         // Scare ID Array

    private Monster monsterSetup;                                                       // Monster Class Reference

    void OnEnable()
    {
        GameEvents.Instance.OnLevelComplete += Terminate;
    }

    void OnDisable()
    {
        GameEvents.Instance.OnLevelComplete -= Terminate;
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterSetup = GetComponent<Monster>();

        StartCoroutine(JumpscareCounter());
    }

    IEnumerator JumpscareCounter()
    {
        float currentTimer = jumpscareTimer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);

            if (!monsterSetup.IsTransitioning)
                currentTimer--;
        }

        MonsterScare();
    }

    /// <summary>
    /// Provides a Subtle Jumpscare for the Player
    /// </summary>
    void MonsterScare()
    {
        // Random Number Generation
        int randomNumberProbability = Random.Range(1, 101);
        int randomNumberScareIndex = Random.Range(0, scareIds.Length);

        Debug.Log("Monster Scare Probability: " + scareProbability + " | " + "Random Number: " + randomNumberProbability);

        if (scareProbability > randomNumberProbability)
            GameUIController.Instance.SetAnimation(scareIds[randomNumberScareIndex]);

        StartCoroutine(JumpscareCounter());
    }

    void Terminate()
    {
        StopAllCoroutines();
    }
}