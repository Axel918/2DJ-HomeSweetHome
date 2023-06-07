using UnityEngine;

public class DotHolder : MonoBehaviour
{
    void Reset()
    {
        Dot[] dots = new Dot[transform.childCount];

        // Rename all Dot Children And Assign Dot Number
        for (int i = 0; i < dots.Length; i++)
        {
            int assignedNumber = i + 1;
            
            // Rename the Dot Prefab in the Hierarchy
            transform.GetChild(i).name = "Dot" + assignedNumber.ToString();
            
            // Get Dot Class Reference
            dots[i] = transform.GetChild(i).GetComponent<Dot>();
            
            // Rename the Dot Number in the Text
            dots[i].SetDotNumber(assignedNumber);
        }
    }
}
