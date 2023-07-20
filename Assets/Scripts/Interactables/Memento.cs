using System.Collections;
using UnityEngine;

public class Memento : Interactable
{
    public bool IsBeingGazed { get; set; }

    protected override void Awake()
    {
        base.Awake();

        IsBeingGazed = false;
    }

    protected override void Examine()
    {
        base.Examine();

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
}