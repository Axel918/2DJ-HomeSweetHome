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
    public bool IsPlaying { get; private set; } = false;
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
        GameManager.Instance.PlayerIsSafe = true;

        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
        {
            ReturnToOverworld();
            return;
        }

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

        for (int i = 0; i < controlType.Length; i++)
            controlType[i].SetActive(i == randomIndex);
    }

    IEnumerator DecreaseGradually()
    {
        IsPlaying = true;
        
        while (IsPlaying)
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
            Debug.Log("Player Stabilized");
            GameManager.Instance.SetGameState(GameManager.GameState.NO_MONSTER);
            PlayerEvents.Instance.PlayerStabilized();

            ReturnToOverworld();
        }
    }

    void ReturnToOverworld()
    {
        ClearData();
        PanelManager.Instance.ActivatePanel("Game UI");
        PlayerEvents.Instance.SetPlayerEnable(true);
        PlayerManager.Instance.Player.PlayerMovement.IsPlayingMiniGame = false;
    }

    void ClearData()
    {
        StopAllCoroutines();

        GameManager.Instance.PlayerIsSafe = false;

        teddyBear.IsBeingUsed = false;
        teddyBear.Cam.SetActive(false);

        teddyBear = null;
        IsPlaying = false;
        currentAmount = 0f;
    }
}