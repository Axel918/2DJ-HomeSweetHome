using UnityEngine;

/// <summary>
/// Refers to a Specific Segment in a Line
/// </summary>
public class Dot : MonoBehaviour
{
    private bool isConnected;                                       // Connection Indicator
    private Pattern pattern;                                        // Pattern Class Reference

    // Start is called before the first frame update
    void Start()
    {
        isConnected = false;
        pattern = transform.GetComponentInParent<Pattern>();
    }

    void OnMouseOver()
    {
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
    /// Connect this Dot to the Line Pattern
    /// </summary>
    public void Connect()
    {
        // Declare this Dot as Connected and Add to
        // the TempPoints List
        isConnected = true;
        pattern.TempDots.Add(this);

        // Update the Line Pattern
        pattern.SetLine();
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
