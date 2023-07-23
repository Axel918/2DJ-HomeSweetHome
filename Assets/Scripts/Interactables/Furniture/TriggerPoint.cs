using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    [SerializeField] private Interactable interactable;                             // Interactable Object Class Reference

    void Update()
    {
        // Deactivate When Player Clicked Somewhere Else
        if (PlayerManager.Instance.Player.NavMeshAgent.destination.x != transform.position.x &&
            PlayerManager.Instance.Player.NavMeshAgent.destination.z != transform.position.z)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Interact with Object Upon Collision
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.State != GameManager.GameState.LEVEL_FAILED)
            {
                StartCoroutine(interactable.Activate());
                PlayerManager.Instance.Player.PlayerMovement.IsPlayingMiniGame = true;
            }
        }
    }
}