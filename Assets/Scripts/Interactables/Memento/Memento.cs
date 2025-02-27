using System.Collections;
using UnityEngine;

public class Memento : Interactable
{
    public bool IsBeingGazed { get; set; }                                  // Indicates if Player is Currently Viewing this Memento

    void OnEnable()
    {
        PlayerEvents.Instance.OnPlayerInsane += Terminate;
    }

    void OnDisable()
    {
        PlayerEvents.Instance.OnPlayerInsane -= Terminate;
    }

    protected override void Awake()
    {
        base.Awake();

        IsBeingGazed = false;
    }

    protected override void Examine()
    {
        base.Examine();

        if (GameManager.Instance.State == GameManager.GameState.MONSTER_PRESENT)
            return;

        if (IsBeingGazed)
            return;

        triggerPoint.SetActive(true);
        PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(triggerPoint.transform.position);
    }

    public override IEnumerator Activate()
    {
        StartCoroutine(base.Activate());

        IsBeingGazed = true;

        yield return new WaitForSeconds(miniGameStartTime);

        PanelManager.Instance.ActivatePanel("Memento Gazing");
        MementoGazing.Instance.Initialize(this);
    }

    /// <summary>
    /// Cancels Currently Viewed Memento
    /// </summary>
    void Terminate()
    {
        StopAllCoroutines();
        Cam.SetActive(false);
        IsBeingGazed = false;
        PlayerEvents.Instance.SetPlayerEnable(true);
    }
}