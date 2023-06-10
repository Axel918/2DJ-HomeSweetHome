using UnityEngine;

public class PatternTriggerPoint : MonoBehaviour
{
    [SerializeField] private PatternFurniture patternFurniture;             // PatternFurniture Class Reference

    void Update()
    {
        // Deactivate When Player Clicked Somewhere Else
        if (PlayerManager.Instance.Player.NavMeshAgent.destination.x != transform.position.x &&
            PlayerManager.Instance.Player.NavMeshAgent.destination.z != transform.position.z)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (patternFurniture.IsComplete)
            return;

        if (other.CompareTag("Player"))
            // Execute Pattern Mini-Game Upon Collision
            StartCoroutine(patternFurniture.EnablePatternMiniGame());
    }
}
