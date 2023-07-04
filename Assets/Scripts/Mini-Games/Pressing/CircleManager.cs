using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private int circleAmount;

    private int currentCircleCount;
    
    void Start()
    {
        currentCircleCount = 0;
    }

    public void Evaluate()
    {
        currentCircleCount++;

        if (currentCircleCount >= circleAmount)
            StartCoroutine(GestureMiniGame.Instance.NextPattern());
    }
}
