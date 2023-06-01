using UnityEngine;

public class Player : MonoBehaviour
{
    public Drawing Drawing { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    
    void Awake()
    {
        // Cache-In Variables
        Drawing = GetComponent<Drawing>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }
}
