using System.Collections;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MonsterCountdown());
    }

    IEnumerator MonsterCountdown()
    {
        float currentTimer = timer;

        while (currentTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
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
        }
        else
        {
            Debug.Log("You're Safe!");
            StartCoroutine(MonsterCountdown());
        }
    }
}