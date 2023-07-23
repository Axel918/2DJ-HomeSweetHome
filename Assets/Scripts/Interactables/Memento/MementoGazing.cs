using UnityEngine;

public class MementoGazing : MonoBehaviour
{
    public static MementoGazing Instance;
    
    private Memento memento;                                        // Memento Class Reference

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += ReturnToOverworld;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= ReturnToOverworld;
    }

    public void Initialize(Memento reference)
    {
        memento = reference;

        if (GameManager.Instance.State == GameManager.GameState.MONSTER_PRESENT)
        {
            ReturnToOverworld();
            return;
        }
    }

    void ClearData()
    {
        // Nullify References
        memento = null;
    }

    public void OnReturnButtonClicked()
    {
        AudioManager.Instance.Play("Button Sound");
        ReturnToOverworld();
    }

    void ReturnToOverworld()
    {
        memento.IsBeingGazed = false;
        memento.Cam.SetActive(false);

        ClearData();

        PanelManager.Instance.ActivatePanel("Game UI");
        PlayerEvents.Instance.SetPlayerEnable(true);
        PlayerManager.Instance.Player.PlayerMovement.IsPlayingMiniGame = false;
    }
}