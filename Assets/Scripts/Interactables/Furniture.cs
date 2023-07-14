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
    private bool inProgress;                                                            // Indicates if this Mini-Game Instance is Currently
                                                                                        // Being Played

    /*private void OnEnable()
    {
        if (PlayerManager.Instance.PlayerData.FurnitureList.Contains(furnitureId))
            Completed();
    }*/

    protected override void Awake()
    {
        base.Awake();

        IsComplete = false;
        inProgress = false;
    }

    /*void OnMouseEnter()
    {   
        if (IsComplete)
            return;

        if (inProgress)
            return;

        //GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        if (IsComplete)
            return;

        if (inProgress)
            return;

        //GetComponent<Renderer>().material.color = Color.white;
    }*/

    protected override void Examine()
    {
        base.Examine();

        if (GameManager.Instance.State == GameManager.GameState.MONSTER_PRESENT)
            return;

        if (IsComplete)
            return;

        if (inProgress)
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

        inProgress = true;

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
        inProgress = false;
        IsComplete = true;
        // Animator.SetTrigger("isComplete");
        Destroy(triggerPoint.gameObject);
        Cam.SetActive(false);
    }

    public void Failed()
    {
        inProgress = false;
        TriggerAnimation(0);
    }

    public void TriggerAnimation(int value)
    {
        Animator.SetInteger("furnitureStatus", value);
    }
}