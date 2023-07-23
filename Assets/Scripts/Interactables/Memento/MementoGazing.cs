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

    public void Initialize(Memento reference)
    {
        memento = reference;

        // Subscribe to Events
        PlayerEvents.Instance.OnPlayerInsane += OnReturnButtonClicked;
    }

    void ClearData()
    {
        // Nullify References
        memento = null;

        // Unsubscribe Events
        PlayerEvents.Instance.OnPlayerInsane -= OnReturnButtonClicked;
    }

    public void OnReturnButtonClicked()
    {
        memento.IsBeingGazed = false;
        memento.Cam.SetActive(false);

        ClearData();

        PanelManager.Instance.ActivatePanel("Game UI");
        PlayerEvents.Instance.SetPlayerEnable(true);
        PlayerManager.Instance.Player.PlayerMovement.IsPlayingMiniGame = false;
    }
}