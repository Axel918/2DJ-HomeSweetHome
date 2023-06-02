using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;                            // Main Camera Reference
    
    private NavMeshAgent navMeshAgent;                              // NavMesh Agent Component Reference
    private bool canMove;                                           // Indicates if Player Can Move

    void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement += value => canMove = value;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerMovement -= value => canMove = value;
    }


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        canMove = true;
        navMeshAgent.updateRotation = false;
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
        navMeshAgent.SetDestination(target);
    }
}