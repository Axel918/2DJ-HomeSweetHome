using System.Collections;
using UnityEngine;
using Cinemachine;

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
    private float miniGameStartTime;                                                    // Time Delay for Starting the Mini-Game

    void Awake()
    {
        IsComplete = false;
        inProgress = false;
        patternCamera.SetActive(false);
        miniGameStartTime = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;
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
    public IEnumerator EnablePatternMiniGame()
    {
        inProgress = true;
        PlayerEvents.Instance.SetPlayerMovement(false);
        patternCamera.SetActive(true);

        yield return new WaitForSeconds(miniGameStartTime);

        PanelManager.Instance.ActivatePanel("Pattern Mini-Game");
        PatternMiniGame.Instance.Initialize(patternData, this, timer);
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