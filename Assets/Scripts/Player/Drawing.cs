using UnityEngine;

public class Drawing : MonoBehaviour
{
    public bool IsDrawing { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        IsDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PatternMiniGame.Instance.CanDraw)
        {
            IsDrawing = false;
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            IsDrawing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsDrawing = false;
        }
    }
}