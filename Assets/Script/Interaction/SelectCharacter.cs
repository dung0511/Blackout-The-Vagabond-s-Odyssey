using UnityEngine;

public class SelectableCharacter : Interactable
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        base.Interact();
        HomePlayerController.Instance.ResetCurrentCharacter();
        HomePlayerController.Instance.selectedCharacter = transform.parent.gameObject;
        HomePlayerController.Instance.RebindCharacter();
    }

    public override void HighLightOn()
    {
        base.HighLightOn();
    }

    public override void HighLightOff()
    {
        base.HighLightOff();
    }
}
