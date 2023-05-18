using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private bool isConnected;

    private Pattern pattern;

    // Start is called before the first frame update
    void Start()
    {
        isConnected = false;
        pattern = transform.GetComponentInParent<Pattern>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (!PlayerManager.Instance.Player.IsDrawing)
            return;

        if (isConnected)
            return;

        Connect();
    }


    public void Connect()
    {
        isConnected = true;
        pattern.TempPoints.Add(this);

        pattern.SetLine();

        Debug.Log("Is Connected!");
    }

    public void Disconnect()
    {
        isConnected = false;
    }
}
