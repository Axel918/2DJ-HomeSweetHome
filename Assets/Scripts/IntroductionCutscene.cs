using System.Collections;
using UnityEngine;
using Cinemachine;

public class IntroductionCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mirrorCamera;                           // Mirror Camera GameObject Reference

    private float spawnTimerStartDelay;                                         // Time Delay for the SpawnTimer 

    // Start is called before the first frame update
    IEnumerator Start()
    {
        spawnTimerStartDelay = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;

        // Show Introduction
        mirrorCamera.SetActive(true);
        GameEvents.Instance.PauseSpawnTimer(true);
        PlayerEvents.Instance.SetPlayerEnable(false);
        
        yield return new WaitForSeconds(3f);

        // Enable Player Movement and Start the Game
        mirrorCamera.SetActive(false);
        PlayerEvents.Instance.SetPlayerEnable(true);

        yield return new WaitForSeconds(spawnTimerStartDelay);
        
        // Start the Timer After Delay
        GameEvents.Instance.PauseSpawnTimer(false);
    }
}
