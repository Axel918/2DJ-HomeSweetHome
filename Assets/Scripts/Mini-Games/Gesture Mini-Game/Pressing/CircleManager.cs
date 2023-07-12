using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int circleAmount;
    [SerializeField] private GameObject circlePrefab;

    private int currentCircleCount;
    
    void Start()
    {
        currentCircleCount = 0;

        for (int i = 0; i < circleAmount; i++)
        {
            // Spawn Circles
            RectTransform go = Instantiate(circlePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<RectTransform>();

            // Generate Random Position within the Panel
            float posX = Random.Range(-850, 850);
            float posY = Random.Range(-400, 400);

            go.localPosition = new Vector2(posX, posY);
        }
    }

    public void Evaluate()
    {
        // Increment Circle Count
        currentCircleCount++;
        
        // If Player Clicked all Buttons, Proceed to Next Pattern
        if (currentCircleCount >= circleAmount)
            StartCoroutine(GestureMiniGame.Instance.NextPattern());
    }
}