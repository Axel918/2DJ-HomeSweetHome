using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;                                        // Main Camera Reference

    private Player playerSetup;                                                 // Player Class Reference
    private bool canMove;                                                       // Indicates if Player Can Move

    void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement += value => canMove = value;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement -= value => canMove = value;
    }

    void Start()
    {
        playerSetup = GetComponent<Player>();
        playerSetup.NavMeshAgent.updateRotation = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        if (Input.GetMouseButtonDown(0))
            PointAndClick();
    }

    /// <summary>
    /// Provides a Top-Down Point and Click Movement Behavior
    /// </summary>
    void PointAndClick()
    {
        // Convert Cursor Screen Position to World Position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Set the Target Position Only If
            // Player Clicked the Ground
            if (hit.collider.CompareTag("Ground"))
                SetTargetPosition(hit.point);
        }
    }

    /// <summary>
    /// Sets the Player's Target Destination
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetPosition(Vector3 target)
    {
        playerSetup.NavMeshAgent.SetDestination(target);
    }
}