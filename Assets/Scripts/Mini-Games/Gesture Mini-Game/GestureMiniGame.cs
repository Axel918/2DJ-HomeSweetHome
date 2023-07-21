using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureMiniGame : MonoBehaviour
{
    public static GestureMiniGame Instance;

    [Header("References")]
    [SerializeField] private Transform patternHolder;                                   // Pattern Holder Point Reference
    [SerializeField] private Image timerBar;                                            // Timer Bar Reference
    [SerializeField] private CanvasGroup canvasGroup;                                   // Canvas Group Component Reference
    private GameObject[] currentPatternData;                                            // Pattern Data Array
    private Furniture currentPatternFurniture;                                          // PatternFurniture Instance Class Reference

    private int currentPatternIndex;                                                    // Current Pattern Index Number
    private List<GameObject> patterns = new();                                          // Pattern Instance List
    private float currentTimer;                                                         // Mini-Game Timer

    private bool isPaused;                                                              // Pause Timer Indicator

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
    /// Initializes All Values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="reference"></param>
    public void Initialize(GameObject[] data, Furniture reference, float timerDuration)
    {
        // Preparatory Clean-up
        ClearData();
        
        // Initialize Variables
        currentPatternData = data;
        currentPatternFurniture = reference;
        currentTimer = timerDuration;
        currentPatternIndex = 0;
        timerBar.fillAmount = 1f;
        isPaused = false;

        // Subscribe Event
        PlayerEvents.Instance.OnPlayerInsane += ReturnToOverworld;

        // Start the Timer
        StartCoroutine(StartTimer());

        // Spawn All Provided Patterns based on Given Pattern Data
        for (int i = 0; i < currentPatternData.Length; i++)
        {
           // Spawn Pattern Prefab
           RectTransform go = Instantiate(data[i], Vector3.zero, Quaternion.identity, patternHolder.transform).GetComponent<RectTransform>();
           
           // Fix Pattern GameObject Position
           go.anchoredPosition3D = Vector3.zero;
           
           // Add Spawned Pattern to the List
           patterns.Add(go.gameObject);
        }

        // Activate First Pattern
        ActivatePattern();
    }

    /// <summary>
    /// Go to the Next Pattern
    /// </summary>
    public IEnumerator NextPattern()
    {        
        // Increment Current Pattern Index
        currentPatternIndex++;

        canvasGroup.alpha = 0f;

        currentPatternFurniture.TriggerAnimation(currentPatternIndex);

        isPaused = true;

        // Check if the Next Pattern is the Last Pattern
        if (currentPatternIndex >= currentPatternData.Length)
        {
            // Mini-Game Finished Successfully,
            // Terminate Mini-Game
            Debug.Log("FINISHED");

            yield return new WaitForSeconds(1f);

            isPaused = false;
            OnSuccess();
            yield break;
        }

        yield return new WaitForSeconds(1f);

        canvasGroup.alpha = 1f;
        isPaused = false;

        // Proceed to the Next Pattern
        ActivatePattern();
    }

    /// <summary>
    /// Cleans Up All Necessary Data for this Class
    /// </summary>
    void ClearData()
    {
        // Destroy Each Pattern Prefab in the List
        foreach(GameObject go in patterns)
            Destroy(go);
        
        // Clear the Pattern List
        patterns.Clear();

        // Nullify All Data and References
        currentPatternData = null;
        currentPatternFurniture = null;
    }

    /// <summary>
    /// Activates the Upcoming Pattern
    /// </summary>
    void ActivatePattern()
    {
        for (int i = 0; i < patterns.Count; i++)
            patterns[i].SetActive(i == currentPatternIndex);
    }

    /// <summary>
    /// Starts the Countdown for the Mini-Game
    /// </summary>
    /// <returns></returns>
    IEnumerator StartTimer()
    {
        float currentTime = currentTimer;
        
        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(0.01f);
            
            if (!isPaused)
            {
                // Decrease Timer Gradually
                currentTime -= 0.01f;

                // Update Timer Bar
                timerBar.fillAmount = currentTime / currentTimer;
            }
        }

        Debug.Log("Time's Up!");
        OnFail();
    }

    #region Winning Conditions
    /// <summary>
    /// Gets Executed If Player Wins the Mini-Game
    /// </summary>
    void OnSuccess()
    {
        // currentPatternFurniture.Register();
        currentPatternFurniture.Completed();
        currentPatternFurniture.InProgress = false;

        // Player Chime SFX
        AudioManager.Instance.PlayOneShot("Mini Game C Chime");

        // Unsubscribe Event
        PlayerEvents.Instance.OnPlayerInsane -= ReturnToOverworld;

        ReturnToOverworld();
    }

    /// <summary>
    /// Gets Executed If Player Loses the Mini-Game
    /// </summary>
    void OnFail()
    {
        currentPatternFurniture.Failed();
        currentPatternFurniture.InProgress = false;

        PlayerEvents.Instance.PlayerDamaged(20f);

        // Unsubscribe Event
        PlayerEvents.Instance.OnPlayerInsane -= ReturnToOverworld;

        ReturnToOverworld();
    }
    #endregion

    /// <summary>
    /// Enables Player Movement and Shifts Camera Back to World View
    /// </summary>
    void ReturnToOverworld()
    {
        StopAllCoroutines();
        ClearData();
        PanelManager.Instance.ActivatePanel("Game UI");
        PlayerEvents.Instance.SetPlayerEnable(true);
    }
}