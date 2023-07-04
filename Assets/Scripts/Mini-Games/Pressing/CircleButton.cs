using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleButton : MonoBehaviour
{
    private CircleManager circleManager;
    
    void Start()
    {
        circleManager = transform.GetComponentInParent<CircleManager>();
    }

    public void OnCircleButtonClicked()
    {
        circleManager.Evaluate();
        gameObject.SetActive(false);
    }
}
