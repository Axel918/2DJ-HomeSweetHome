using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    [SerializeField] private Animator transitionAnimator;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("Game UI");
        transitionAnimator.gameObject.SetActive(true);
    }

    public void SetAnimation(string id)
    {
        transitionAnimator.SetTrigger(id);
    }
}
