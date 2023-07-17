using System.Collections;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TeleportTrigger targetTeleportTrigger;                         // TeleportTrigger Target Class Reference

    public bool Teleported { get; set; } = false;                                           // Indicates if Player Has Teleported from this Point

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            // Start Teleporting Upon Entry
            StartCoroutine(BeginTeleporting(other));
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            // Declare As Unteleported Upon Exit
            Teleported = false;
    }

    /// <summary>
    /// Start Teleporting Process
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator BeginTeleporting(Collider other)
    {
        // Check if Player Has Not yet Teleported from this Point
        if (!Teleported)
        {
            // Trigger Fade Transition
            GameUIController.Instance.SetAnimation("isFading");

            // Pause the Spawn Timer
            GameEvents.Instance.PauseSpawnTimer(true);

            yield return new WaitForSeconds(1f);

            // Declare Player As Teleported from this Point
            targetTeleportTrigger.Teleported = true;

            // Teleport the Player to the Designated Position
            other.transform.position = targetTeleportTrigger.transform.position;
            PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(targetTeleportTrigger.transform.position);

            // Unpause the Spawn Timer
            GameEvents.Instance.PauseSpawnTimer(false);
        }
    }
}