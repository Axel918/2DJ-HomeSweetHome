using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTrigger : MonoBehaviour
{
    [Header("Gesture Pattern Library")]
    [SerializeField] private GameObject[] patternData;

    private bool isComplete;



    // Start is called before the first frame update
    void Start()
    {
        isComplete = false;
    }

    void Update()
    {
        if (isComplete)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnablePatternMiniGame();
        }
    }

    void EnablePatternMiniGame()
    {
        PatternMiniGame.Instance.Initialize(patternData, this);
        isComplete = true;
    }

    public void Completed()
    {
        isComplete = true;
    }
}
