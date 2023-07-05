using UnityEngine;
using UnityEngine.UI;

public class DragSlider : MonoBehaviour
{
    private Slider slider;                                                  // Slider Component Reference
    private float currentAmount = 0f;                                       // Slider Amount Tracker


    void Awake()
    {
        slider = GetComponent<Slider>();
        currentAmount = 0f;
    }

    /// <summary>
    /// Ensures that the Slider Handle Doesn't Get Dragged Backward
    /// </summary>
    public void Clamp()
    {
        if (currentAmount < slider.value)
        {
            currentAmount = slider.value;
        }
        else if (currentAmount > slider.value)
        {
            slider.value = currentAmount;
        }

        Debug.Log(currentAmount);
    }

    /// <summary>
    /// Checks if Player Dragged the Handle to the End
    /// </summary>
    public void Checking()
    {
        // Check if Slider has Reached Maximum Value
        if (slider.value >= 1f)
        {
            Debug.Log("Finished!");

            // Declare as Finished
            StartCoroutine(GestureMiniGame.Instance.NextPattern());
        }
        else
        {
            // Reset Silder Value and Amount Tracker
            currentAmount = 0f;
            slider.value = 0f;
            
            Debug.Log("Reset");
        }
    }
}
