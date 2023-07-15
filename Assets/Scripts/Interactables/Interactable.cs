using System.Collections;
using UnityEngine;
using Cinemachine;

public abstract class Interactable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject triggerPoint;
    public GameObject Cam;

    protected float miniGameStartTime;

    protected virtual void Awake()
    {
        triggerPoint.SetActive(false);
        Cam.SetActive(false);
        miniGameStartTime = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;
    }

    void OnMouseEnter()
    {
        
    }

    void OnMouseExit()
    {
        
    }

    void OnMouseDown()
    {
        // TO BE REMOVED!!!
        Examine();
    }

    /// <summary>
    /// TO BE REMOVED!!!
    /// </summary>
    protected virtual void Examine()
    {
    
    }

    public virtual IEnumerator Activate()
    {
        PlayerEvents.Instance.SetPlayerEnable(false);
        Cam.SetActive(true);

        Debug.Log("ACTIVATE");

        yield return null;
    }
}