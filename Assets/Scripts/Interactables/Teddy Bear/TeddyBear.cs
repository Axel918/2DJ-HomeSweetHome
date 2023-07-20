using System.Collections;
using UnityEngine;

public class TeddyBear : Interactable
{
    public bool IsBeingUsed { get; set; }

    protected override void Awake()
    {
        base.Awake();

        IsBeingUsed = false;
    }

    protected override void Examine()
    {
        base.Examine();

        if (GameManager.Instance.State != GameManager.GameState.MONSTER_PRESENT)
            return;

        if (IsBeingUsed)
            return;

        triggerPoint.SetActive(true);
        PlayerManager.Instance.Player.PlayerMovement.SetTargetPosition(triggerPoint.transform.position);
    }

    public override IEnumerator Activate()
    {
        StartCoroutine(base.Activate());

        IsBeingUsed = true;

        yield return new WaitForSeconds(miniGameStartTime);

        PanelManager.Instance.ActivatePanel("Soothing Mini-Game");
        SoothingMiniGame.Instance.Initialize(this);
    }
}
