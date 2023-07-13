using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    Vector3 mousePos;

    private float currentAmount = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrag()
    {
        mousePos = Input.mousePosition;

        Vector2 dir = mousePos - transform.position;

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        // angle = (angle <= 0f) ? angle = 360f : angle;

        //Debug.Log(transform.localEulerAngles / 360f);


        //Debug.Log(transform.rotation.z);

        if (currentAmount < transform.rotation.z)
        {
            currentAmount += transform.rotation.z;
        }

        Debug.Log("Current Amount: " + currentAmount);

        /*if (currentAmount < transform.localEulerAngles.z / 360f)
        {
            transform.localEulerAngles = new Vector3(0, 0, -angle);
        }
        else if (currentAmount > 1 - (transform.localEulerAngles.z / 360f))
        {
            transform.localEulerAngles = new Vector3(0, 0, -angle);
        }*/

        transform.localEulerAngles = new Vector3(0, 0, -angle);
    }
}