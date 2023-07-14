using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform teleportTrigger;                     // Teleport Trigger Transform Reference
    
    void OnMouseDown()
    {
        PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(teleportTrigger.position);
    }   
}
