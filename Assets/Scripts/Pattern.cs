using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("References")]
    public LineRenderer LineRenderer;                                           // Line Renderer GameObject Reference
    [SerializeField] private Transform dotHolder;                               // Dot Holder Parent

    public List<Dot> TempDots { get; private set; } = new();                    // List Of Temporary Dot Connection
    private Dot[] dots;                                                         // Correct Dot Sequence Code

    // Start is called before the first frame update
    void Start()
    {
        dots = dotHolder.GetComponentsInChildren<Dot>();
    }

    // Update is called once per frame
    void Update()
    {
        // On Left Mouse Button Released
        if (Input.GetMouseButtonUp(0))
        {
            // If Complete, Check if right or wrong
            // If not, restart
            if (TempDots.Count != dots.Length)
            {
                ResetDots();
                return;
            }

            // Initiate Evaluation
            StartCoroutine(StartEvaluating());   
        }
    }

    /// <summary>
    /// Checks if Drawn Pattern Shows the Correct Shape and
    /// Written in Proper Order
    /// </summary>
    /// <returns></returns>
    IEnumerator StartEvaluating()
    {
        yield return new WaitForSeconds(0.5f);
        
        // Check Every Dot Segment
        for (int i = 0; i < dots.Length; i++)
        {
            if (TempDots[i] != dots[i])
            {
                // Declare Drawn Pattern as Wrong
                Debug.Log("WRONG");
                yield return new WaitForSeconds(1f);

                // Reset Dots and End Evaluation
                ResetDots();
                yield break;
            }
        }

        // Declare Drawn Pattern as Correct
        Debug.Log("CORRECT");

        yield return new WaitForSeconds(1f);
        
        // Reset the Dots After Slight Delay
        ResetDots();
    }

    /// <summary>
    /// Resets Dot Values
    /// </summary>
    void ResetDots()
    {
        // Clear Temporary Dot List
        TempDots.Clear();
        
        // Reset Line Segment Count
        LineRenderer.positionCount = TempDots.Count;

        // Disconnect All Dots
        for (int i = 0; i < dots.Length; i++)
            dots[i].Disconnect();
    }

    /// <summary>
    /// Updates Line Renderer to the Current Dot Count
    /// </summary>
    public void SetLine()
    {
        // For Every Dot in the TempDot List
        for (int i = 0; i < TempDots.Count; i++)
        {
            // Set Total Line Renderer Segments to the Current TempDot List Count
            LineRenderer.positionCount = TempDots.Count;

            // Set Segment Positions
            LineRenderer.SetPosition(i, TempDots[i].transform.position);
        }
    }
}