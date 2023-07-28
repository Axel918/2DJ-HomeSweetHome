using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;                                        // Main Camera Reference

    public bool IsPlayingMiniGame { get; set; } = false;                        // Indicates if Player is Currently Playing a Mini-Game

    private Player playerSetup;                                                 // Player Class Reference
    private bool canMove;                                                       // Indicates if Player Can Move
    private Vector3 moveDirection;                                              // Current Movement Direction
    private Vector3 lastMoveDirection;                                          // Latest Movement Direction

    void OnEnable()
    {
        PlayerEvents.Instance.OnSetPlayerEnable += value => canMove = value;
        GameEvents.Instance.OnLevelFailed += OnPlayerAttacked;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnSetPlayerEnable -= value => canMove = value;
        GameEvents.Instance.OnLevelFailed -= OnPlayerAttacked;
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
        if (GameUIController.Instance.PauseMenu.GameIsPaused)
            return;
        
        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
            return;
        
        Animate();
        
        if (!canMove)
            return;

        if (!playerSetup.NavMeshAgent.enabled)
            return;

        // Go to Location where the Player Clicked
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
    /// Executes Player Animation
    /// </summary>
    void Animate()
    {
        // Set Value of Latest Move Direction
        if ((playerSetup.NavMeshAgent.velocity.x == 0f && playerSetup.NavMeshAgent.velocity.z == 0f) && playerSetup.NavMeshAgent.velocity.x != 0f || playerSetup.NavMeshAgent.velocity.z != 0f)
            lastMoveDirection = moveDirection;

        // Set-Up and Normalize Movement Direction
        moveDirection = new Vector3(playerSetup.NavMeshAgent.velocity.x, 0f, playerSetup.NavMeshAgent.velocity.z).normalized;
        
        // Modify Move Directions in X and Z-Axis
        playerSetup.Animator.SetFloat("moveX", moveDirection.x);
        playerSetup.Animator.SetFloat("moveZ", moveDirection.z);

        // Indicate if Player is Moving or Not
        playerSetup.Animator.SetFloat("moveMagnitude", moveDirection.sqrMagnitude);

        // Modify Latest Move Direction
        playerSetup.Animator.SetFloat("lastMoveX", lastMoveDirection.x);
        playerSetup.Animator.SetFloat("lastMoveZ", lastMoveDirection.z);
    }

    /// <summary>
    /// Sets the Player's Target Destination
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetPosition(Vector3 target)
    {
        playerSetup.NavMeshAgent.SetDestination(target);
    }

    void OnPlayerAttacked()
    {
        playerSetup.Animator.SetTrigger("isAttacked");
    }
}