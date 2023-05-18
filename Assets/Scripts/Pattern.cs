using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public List<Dot> TempPoints { get; private set; } = new();
    public LineRenderer LineRenderer;


    [SerializeField] private Transform dotHolder;
    private Dot[] points;

    // Start is called before the first frame update
    void Start()
    {
        points = dotHolder.GetComponentsInChildren<Dot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // If Complete, Check if right or wrong
            // If not, restart
            if (TempPoints.Count != points.Length)
            {
                ResetDots();
                Debug.Log("Reset");
                return;
            }

            StartCoroutine(StartEvaluating());   
        }
    }

    IEnumerator StartEvaluating()
    {
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < points.Length; i++)
        {
            if (TempPoints[i] != points[i])
            {
                Debug.Log("WRONG");
                yield return new WaitForSeconds(1f);
                ResetDots();
                yield break;
            }
        }

        Debug.Log("CORRECT");

        yield return new WaitForSeconds(1f);

        ResetDots();
    }

    void ResetDots()
    {
        TempPoints.Clear();
        LineRenderer.positionCount = TempPoints.Count;

        for (int i = 0; i < points.Length; i++)
            points[i].Disconnect();
    }

    public void SetLine()
    {
        for (int i = 0; i < TempPoints.Count; i++)
        {
            LineRenderer.positionCount = TempPoints.Count;
            LineRenderer.SetPosition(i, TempPoints[i].transform.position);
        }
    }
}