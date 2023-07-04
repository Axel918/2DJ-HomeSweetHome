using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoothingMiniGame : MonoBehaviour
{
    public static SoothingMiniGame Instance;

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
    }

    public void OnButtonPressed()
    {
        currentAmount++;

        stabilizeBar.fillAmount = currentAmount / reqAmount;

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
