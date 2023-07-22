using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject triggerPoint;                         // TriggerPoint GameObject Reference
    public GameObject Cam;                                                      // Child Camera Reference

    protected float miniGameStartTime;                                          // Start Delay Based on Camera Transition Duration

    protected virtual void Awake()
    {
        triggerPoint.SetActive(false);
        Cam.SetActive(false);
        miniGameStartTime = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;
    }

    void OnMouseEnter()
    {
        if (GameUIController.Instance.PauseMenu.GameIsPaused)
            return;

        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
            return;
    }

    void OnMouseExit()
    {
        if (GameUIController.Instance.PauseMenu.GameIsPaused)
            return;

        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
            return;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (GameUIController.Instance.PauseMenu.GameIsPaused)
            return;

        if (GameManager.Instance.State == GameManager.GameState.LEVEL_FAILED)
            return;
        
        Examine();
    }

    /// <summary>
    /// Player Goes to this Object to Examine It
    /// </summary>
    protected virtual void Examine()
    {
    
    }

    public virtual IEnumerator Activate()
    {
        PlayerEvents.Instance.SetPlayerEnable(false);
        Cam.SetActive(true);

        yield return null;
    }
}