using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        int randomNumber = Random.Range(0, controlType.Length);

        Debug.Log("Random Number: " + randomNumber);

        for (int i = 0; i < controlType.Length; i++)
            controlType[i].SetActive(i == randomNumber);
    }

    public void OnButtonPressed()
    {
        currentAmount++;

        stabilizeBar.fillAmount = currentAmount / reqAmount;

        Evaluate();
    }

    public void OnKnobRotated()
    {
        currentAmount += 0.01f;

        stabilizeBar.fillAmount = currentAmount / reqAmount;

        Evaluate();
    }

    void Evaluate()
    {
        if (currentAmount >= reqAmount)
        {
            teddyBear.IsBeingUsed = false;
            teddyBear.Cam.SetActive(false);

            ClearData();

            Debug.Log("Player Stabilized");
            PanelManager.Instance.ActivatePanel("Game UI");
            PlayerEvents.Instance.SetPlayerMovement(true);
        }
    }

    void ClearData()
    {
        teddyBear = null;
        currentAmount = 0f;
    }
}
