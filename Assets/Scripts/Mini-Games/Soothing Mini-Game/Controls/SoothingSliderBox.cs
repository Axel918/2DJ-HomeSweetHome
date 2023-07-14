using UnityEngine;

public class SoothingSliderBox : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float increaseAmount = 0.01f;

    public void OnHandleDrag()
    {
        SoothingMiniGame.Instance.IncreaseStabilizeBarValue(increaseAmount);
        SoothingMiniGame.Instance.Evaluate();
    }
}
