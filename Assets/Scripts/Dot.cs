using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private bool isConnected;
    
    // Start is called before the first frame update
    void Start()
    {
        isConnected = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        isConnected = true;
        Debug.Log("Is Connected!");
    }

    public void Disconnect()
    {
        isConnected = false;
    }
}
