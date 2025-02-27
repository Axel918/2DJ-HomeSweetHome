using System.Collections;
using UnityEngine;

public class Furniture : Interactable
{
    [Header("References")]
    public Animator Animator;

    [Header("Gesture Library")]
    [SerializeField] private GameObject[] gestureData;                                  // Collection of Gesture Prefabs

    [Header("Properties")]
    [SerializeField] private string furnitureId;                                        // Distinct Furniture ID
    [SerializeField] private float timer = 10f;                                         // Timer Value

    public bool IsComplete { get; set; }                                                // Indicates if thie Mini-Game Instance is Completed
    public bool InProgress { get; set; }                                                // Indicates if this Mini-Game Instance is Currently
                                                                                        // Being Played

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= Terminate;
    }

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.TotalFurniture++;

        IsComplete = false;
        InProgress = false;
    }

    protected override void Examine()
    {
        base.Examine();

        if (GameManager.Instance.State == GameManager.GameState.MONSTER_PRESENT)
            return;

        if (IsComplete)
            return;

        if (InProgress)
            return;

        triggerPoint.SetActive(true);
        PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(triggerPoint.transform.position);
    }

    /// <summary>
    /// Initiates Pattern Mini-Game
    /// </summary>
    public override IEnumerator Activate()
    {
        StartCoroutine(base.Activate());

        InProgress = true;

        yield return new WaitForSeconds(miniGameStartTime); 

        PanelManager.Instance.ActivatePanel("Gesture Mini-Game");
        GestureMiniGame.Instance.Initialize(gestureData, this, timer);
    }

    /// <summary>
    /// Adds ID of this Furniture to the Furniture List to Indicate Completion Status
    /// </summary>
    public void Register()
    {
        PlayerManager.Instance.PlayerData.AddId(furnitureId);
    }

    /// <summary>
    /// Sets this Mini-Game Instance as Completed
    /// </summary>
    public void Completed()
    {
        GameManager.Instance.CheckList();

        InProgress = false;
        IsComplete = true;
        Destroy(triggerPoint.gameObject);
        Cam.SetActive(false);
    }

    /// <summary>
    /// Gets Executed Upon Failure
    /// </summary>
    public void Failed()
    {
        InProgress = false;
        Cam.SetActive(false);
        TriggerAnimation(0);
    }

    /// <summary>
    /// Triggers an Animation Clip of this Furniture
    /// </summary>
    /// <param name="value"></param>
    public void TriggerAnimation(int value)
    {
        Animator.SetInteger("furnitureStatus", value);
    }

    /// <summary>
    /// Cancels Currently Played Furniture
    /// </summary>
    void Terminate()
    {
        if (IsComplete)
            return;
        
        StopAllCoroutines();
        TriggerAnimation(0);
        InProgress = false;
        Cam.SetActive(false);
        PlayerEvents.Instance.SetPlayerEnable(true);
    }
}