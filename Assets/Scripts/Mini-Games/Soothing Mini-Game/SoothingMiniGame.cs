using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoothingMiniGame : MonoBehaviour
{
    public static SoothingMiniGame Instance;

    [Header("Properties")]
    [SerializeField] private GameObject[] controlType;
    [SerializeField] private float reqAmount = 20f;

    [Header("References")]
    [SerializeField] private Image stabilizeBar;

    private TeddyBear teddyBear;
    private float currentAmount = 0f;
    private bool isPlaying = false;
    private int currentRandomIndex = -1;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public void Initialize(TeddyBear reference)
    {
        teddyBear = reference;
        stabilizeBar.fillAmount = 0f;

        RandomizeControlType();

        StartCoroutine(DecreaseGradually());
    }

    void RandomizeControlType()
    {
        // Initialize Random Number
        int randomIndex = Random.Range(0, controlType.Length);

        // Keep on Randomizing until randomIndex Value is Different from the Previous Value
        while (currentRandomIndex == randomIndex)
            randomIndex = Random.Range(0, controlType.Length);

        // Set Current Random Index to the Chosen Random Number Index
        currentRandomIndex = randomIndex;

        Debug.Log("Random Number: " + randomIndex);

        for (int i = 0; i < controlType.Length; i++)
            controlType[i].SetActive(i == randomIndex);
    }

    IEnumerator DecreaseGradually()
    {
        isPlaying = true;
        
        while (isPlaying)
        {
            currentAmount -= 0.01f;

            if (currentAmount < 0f)
                currentAmount = 0f;

            stabilizeBar.fillAmount = currentAmount / reqAmount;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void IncreaseStabilizeBarValue(float amount)
    {
        currentAmount += amount;
        stabilizeBar.fillAmount = currentAmount / reqAmount;
    }

    public void Evaluate()
    {
        if (currentAmount >= reqAmount)
        {
            teddyBear.IsBeingUsed = false;
            teddyBear.Cam.SetActive(false);

            ClearData();

            Debug.Log("Player Stabilized");
            GameManager.Instance.SetGameState(GameManager.GameState.NO_MONSTER);
            PlayerEvents.Instance.PlayerStabilized();
            PanelManager.Instance.ActivatePanel("Game UI");
            PlayerEvents.Instance.SetPlayerMovement(true);
        }
    }

    void ClearData()
    {
        StopAllCoroutines();
        
        teddyBear = null;
        isPlaying = false;
        currentAmount = 0f;
    }
}