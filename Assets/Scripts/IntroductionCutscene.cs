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
        // Player Ambient Music
        AudioManager.Instance.Play("Ambient Sound Final");

        spawnTimerStartDelay = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;

        // Show Introduction
        mirrorCamera.SetActive(true);
        GameEvents.Instance.PauseSpawnTimer(true);
        PlayerEvents.Instance.SetPlayerEnable(false);
        PlayerManager.Instance.Player.PlayerSpriteTransform.localEulerAngles = new Vector3 (0f, 270f, 0f);
        
        yield return new WaitForSeconds(3f);

        // Provide a Smooth Transition upon Rotation 
        GameUIController.Instance.SetAnimation("isFading");

        yield return new WaitForSeconds(1f);

        // Enable Player Movement and Start the Game
        PlayerManager.Instance.Player.PlayerSpriteTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
        mirrorCamera.SetActive(false);
        PlayerEvents.Instance.SetPlayerEnable(true);
        
        yield return new WaitForSeconds(spawnTimerStartDelay);
        
        // Start the Timer After Delay
        GameEvents.Instance.PauseSpawnTimer(false);
    }
}