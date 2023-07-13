using UnityEngine;
using DG.Tweening;

public class SoothingButton : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float increaseAmount = 1f;

    public void OnSootingButtonPressed()
    {
        SoothingMiniGame.Instance.IncreaseStabilizeBarValue(increaseAmount);

        transform.DOScale(1.5f, 0.25f).SetEase(Ease.OutBounce).OnComplete(() => transform.DOScale(1f, 0.25f));

        SoothingMiniGame.Instance.Evaluate();
    }
}
