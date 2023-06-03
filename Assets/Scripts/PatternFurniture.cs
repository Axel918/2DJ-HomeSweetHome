using UnityEngine;

public class PatternFurniture : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform patternTriggerPoint;

    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;

    public bool IsComplete { get; set; }
    public bool InProgress { get; set; }

    void Awake()
    {
        IsComplete = false;
        InProgress = false;
    }

    void OnMouseEnter()
    {   
        if (IsComplete)
            return;

        if (InProgress)
            return;

        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        if (IsComplete)
            return;

        if (InProgress)
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

    public void EnablePatternMiniGame()
    {
        InProgress = true;
        PlayerEvents.Instance.SetPlayerMovement(false);
        PanelManager.Instance.ActivatePanel("Pattern Mini-Game");
        PatternMiniGame.Instance.Initialize(patternData, this);
    }

    public void Completed()
    {
        IsComplete = true;
        InProgress = false;
        Destroy(patternTriggerPoint.gameObject);
        GetComponent<Renderer>().material.color = Color.gray;
    }
}