using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    void Update()
    {
        // Deactivate When Player Clicked Somewhere Else
        if (PlayerManager.Instance.Player.NavMeshAgent.destination.x != transform.position.x &&
            PlayerManager.Instance.Player.NavMeshAgent.destination.z != transform.position.z)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(interactable.Activate());

        // Execute Pattern Mini-Game Upon Collision
        //StartCoroutine(patternFurniture.EnablePatternMiniGame());
    }
}
