using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    [Header("References")]
    [SerializeField] private Animator transitionAnimator;                               // Transition Animator Component Reference
    [SerializeField] private GameObject uiController;                                   // UI Controller Object Reference
    [SerializeField] private GameObject nonUIController;                                // Non-UI Controller Object Reference

    public DebugMenu DebugMenu { get; private set; }
    public PauseMenu PauseMenu { get; private set; }

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        uiController.SetActive(true);
        nonUIController.SetActive(true);

        DebugMenu = GetComponent<DebugMenu>();
        PauseMenu = GetComponent<PauseMenu>();

        PanelManager.Instance.ActivatePanel("Game UI");
        transitionAnimator.gameObject.SetActive(true);
    }

    public void SetAnimation(string id)
    {
        transitionAnimator.SetTrigger(id);
    }
}
