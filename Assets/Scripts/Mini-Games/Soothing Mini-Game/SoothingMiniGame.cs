using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoothingMiniGame : MonoBehaviour
{
    public static SoothingMiniGame Instance;

    [Header("Properties")]
    [SerializeField] private GameObject[] controlType;
    
    [Header("References")]
    [SerializeField] private Image stabilizeBar;


    private TeddyBear teddyBear;
    private float reqAmount = 20f;
    private float currentAmount = 0f;
    private bool isPlaying = false;

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

        //int randomNumber = Random.Range(0, controlType.Length);

        int randomNumber = 0;

        Debug.Log("Random Number: " + randomNumber);

        for (int i = 0; i < controlType.Length; i++)
            controlType[i].SetActive(i == randomNumber);

        StartCoroutine(DecreaseGradually());
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
