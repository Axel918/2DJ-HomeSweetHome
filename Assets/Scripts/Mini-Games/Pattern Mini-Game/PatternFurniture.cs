using UnityEngine;

public class PatternFurniture : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform patternTriggerPoint;                             // Pattern Trigger Reference
    [SerializeField] private GameObject patternCamera;

    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;                                  // Collection of Pattern Prefabs

    [Header("Properties")]
    [SerializeField] private float timer = 10f;                                         // Timer Value

    public bool IsComplete { get; set; }                                                // Indicates if thie Mini-Game Instance is Completed
    private bool inProgress;                                                            // Indicates if this Mini-Game Instance is Currently
                                                                                        // Being Played

    void Awake()
    {
        IsComplete = false;
        inProgress = false;
        patternCamera.SetActive(false);
    }

    void OnMouseEnter()
    {   
        if (IsComplete)
            return;

        if (inProgress)
            return;

        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        if (IsComplete)
            return;

        if (inProgress)
            return;

        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        if (IsComplete)
            return;

        patternTriggerPoint.gameObject.SetActive(true);
        PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(patternTriggerPoint.position);
    }

    /// <summary>
    /// Initiates Pattern Mini-Game
    /// </summary>
    public void EnablePatternMiniGame()
    {
        inProgress = true;
        PlayerEvents.Instance.SetPlayerMovement(false);
        PanelManager.Instance.ActivatePanel("Pattern Mini-Game");
        PatternMiniGame.Instance.Initialize(patternData, this, timer);
        patternCamera.SetActive(true);
    }

    /// <summary>
    /// Sets this Mini-Game Instance as Completed
    /// </summary>
    public void Completed()
    {
        inProgress = false;
        IsComplete = true;
        Destroy(patternTriggerPoint.gameObject);
        GetComponent<Renderer>().material.color = Color.gray;
        patternCamera.SetActive(false);
    }

    public void Failed()
    {
        inProgress = false;
    }
}