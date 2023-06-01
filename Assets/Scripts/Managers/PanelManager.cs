using UnityEngine;

/// <summary>
/// Manages UI Panels
/// </summary>
public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    
    [System.Serializable]
    // Panel Data Struct
    public struct PanelData
    {
        public string Id;
        public GameObject PanelObject;
    }

    [Header("References")]
    [SerializeField] private PanelData[] panels;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    /// <summary>
    /// Activates Selected Panel Based on ID Input
    /// </summary>
    /// <param name="id"></param>
    public void ActivatePanel(string id)
    {
        // Enable Chosen Panel and disable the rest
        for (int i = 0; i < panels.Length; i++)
            panels[i].PanelObject.SetActive(panels[i].Id == id);
    }
}
