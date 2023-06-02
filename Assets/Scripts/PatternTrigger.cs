using UnityEngine;

public class PatternTrigger : MonoBehaviour
{
    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;

    private bool isComplete;

    // Start is called before the first frame update
    void Start()
    {
        isComplete = false;
    }

    void OnMouseEnter()
    {
        if (isComplete)
            return;

        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        if (isComplete)
            return;

        GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (isComplete)
            return;

        EnablePatternMiniGame();
    }

    void EnablePatternMiniGame()
    {
        PlayerEvents.Instance.SetPlayerMovement(false);
        PanelManager.Instance.ActivatePanel("Pattern Mini-Game");
        PatternMiniGame.Instance.Initialize(patternData, this);
    }

    public void Completed()
    {
        isComplete = true;
        GetComponent<Renderer>().material.color = Color.gray;
    }
}
