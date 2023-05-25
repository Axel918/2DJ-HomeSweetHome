using UnityEngine;

/// <summary>
/// Refers to a Specific Segment in a Line
/// </summary>
public class Dot : MonoBehaviour
{
    private bool isConnected;                                       // Connection Indicator
    private bool onMouseOver;                                       // Indicator that the Cursor is Hovering this Object
    private Pattern pattern;                                        // Pattern Class Reference

    // Start is called before the first frame update
    void Start()
    {
        isConnected = false;
        pattern = transform.GetComponentInParent<Pattern>();
        onMouseOver = false;
    }

    void Update()
    {
        // Don't Connect if Cursor is not Hovering this Object
        if (!onMouseOver)
            return;

        // Don't Connect if Player is Not Drawing
        if (!PlayerManager.Instance.Player.Drawing.IsDrawing)
            return;

        // Don't Connect if Dot has been Connected
        if (isConnected)
            return;

        // Start Connecting
        Connect();
    }

    /// <summary>
    /// Set Mouse Hover Indicator
    /// </summary>
    /// <param name="value"></param>
    public void SetMouseOver(bool value)
    {
        onMouseOver = value;
    }

    /// <summary>
    /// Connect this Dot to the Line Pattern
    /// </summary>
    public void Connect()
    {
        // Declare this Dot as Connected and Add to
        // the TempPoints List
        isConnected = true;
        pattern.TempDots.Add(this);

        // Update the Line Pattern
        pattern.SetLine(transform.localPosition);
    }

    /// <summary>
    /// Disconnect this Dot from the Line Pattern
    /// </summary>
    public void Disconnect()
    {
        // Declare this Dot as Disconnected
        isConnected = false;
    }
}
