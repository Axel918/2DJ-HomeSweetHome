using UnityEngine;

public class CircleButton : MonoBehaviour
{
    private CircleManager circleManager;                                // CircleManager Class Reference
    
    void Start()
    {
        circleManager = transform.GetComponentInParent<CircleManager>();
    }

    public void OnCircleButtonClicked()
    {
        // Update to the CircleManager
        circleManager.Evaluate();

        // Deactivate
        gameObject.SetActive(false);
    }
}
