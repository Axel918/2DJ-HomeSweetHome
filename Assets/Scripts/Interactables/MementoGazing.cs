using UnityEngine;

public class MementoGazing : MonoBehaviour
{
    public static MementoGazing Instance;
    
    private Memento memento;

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
    }

    void ClearData()
    {
        // Nullify References
        memento = null;
    }

    public void OnReturnButtonClicked()
    {
        memento.IsBeingGazed = false;
        memento.Cam.SetActive(false);

        ClearData();

        PanelManager.Instance.ActivatePanel("Game UI");
        PlayerEvents.Instance.SetPlayerMovement(true);
    }
}
