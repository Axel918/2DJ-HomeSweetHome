using UnityEngine;

public class PatternFurniture : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform patternTriggerPoint;

    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;

    public bool IsComplete { get; set; }
    private bool inProgress;

    void Awake()
    {
        IsComplete = false;
        inProgress = false;
    }

    void OnMouseEnter()
    {   
        if (IsComplete)
            return;

        if (inProgress)
            return;

        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
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

    public void EnablePatternMiniGame()
    {
        inProgress = true;
        PlayerEvents.Instance.SetPlayerMovement(false);
        PanelManager.Instance.ActivatePanel("Pattern Mini-Game");
        PatternMiniGame.Instance.Initialize(patternData, this);
    }

    public void Completed()
    {
        IsComplete = true;
        inProgress = false;
        Destroy(patternTriggerPoint.gameObject);
        GetComponent<Renderer>().material.color = Color.gray;
    }
}