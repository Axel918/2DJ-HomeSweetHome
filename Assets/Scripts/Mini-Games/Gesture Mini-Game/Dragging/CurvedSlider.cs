using UnityEngine;

public class CurvedSlider : MonoBehaviour
{
    public enum SpeedType
    {
        SLOW,
        MEDIUM,
        FAST
    }

    [Header("Properties")]
    [SerializeField] private SpeedType speed;                                   // Speed Type Indicator

    private Animator animator;                                                  // Animator Component Reference
    private bool isPlaying = false;                                             // Indicates if Moving Animation is Being Played
    private bool isFinished = false;                                            // Indicates if Moving Animation is Finished

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (isPlaying)
            return;

        isPlaying = true;

        // Play Animation Based on Chosen Speed Type
        switch (speed)
        {
            case SpeedType.SLOW:
                animator.SetTrigger("isSlow");
                break;

            case SpeedType.MEDIUM:
                animator.SetTrigger("isMedium");
                break;

            case SpeedType.FAST:
                animator.SetTrigger("isFast");
                break;
        }
    }

    public void OnAnimationFinished()
    {
        Debug.Log("Animation is Finished");
        isFinished = true;
    }

    #region Winning Conditions
    /// <summary>
    /// Checks if Player Successfully Performs the Pattern
    /// </summary>
    public void Evaluate()
    {   
        if (isFinished)
            OnSuccess();
        else
            OnFail();
    }

    void OnSuccess()
    {
        StartCoroutine(GestureMiniGame.Instance.NextPattern());
    }

    void OnFail()
    {
        // Reset Indicators
        isPlaying = false;
        isFinished = false;

        // Reset Animation Back to Idle
        animator.SetTrigger("isIdle");
    }
    #endregion
}
