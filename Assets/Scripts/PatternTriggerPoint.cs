using UnityEngine;

public class PatternTriggerPoint : MonoBehaviour
{
    [SerializeField] private PatternFurniture patternFurniture;

    void Update()
    {
        if (PlayerManager.Instance.Player.NavMeshAgent.destination.x != transform.position.x &&
            PlayerManager.Instance.Player.NavMeshAgent.destination.z != transform.position.z)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (patternFurniture.IsComplete)
            return;

        if (other.CompareTag("Player"))
            patternFurniture.EnablePatternMiniGame();
    }
}
