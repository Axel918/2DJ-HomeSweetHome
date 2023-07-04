using System.Collections;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Furniture : Interactable
{
    [Header("References")]
    public Animator Animator;

    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;                                  // Collection of Pattern Prefabs

    [Header("Properties")]
    [SerializeField] private float timer = 10f;                                         // Timer Value

    public bool IsComplete { get; set; }                                                // Indicates if thie Mini-Game Instance is Completed
    private bool inProgress;                                                            // Indicates if this Mini-Game Instance is Currently
                                                                                        // Being Played

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

        if (IsComplete)
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
        Destroy(triggerPoint.gameObject);
        //GetComponent<Renderer>().material.color = Color.gray;
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