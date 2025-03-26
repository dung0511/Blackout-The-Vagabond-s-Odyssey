using UnityEngine;

public class PlayableCharacter : Interactable
{
    public CharacterVariantSO selectedCharacter; //them ttin nhan vat vao SO

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact(Interactor interactPlayer)
    {
        base.Interact(interactPlayer);
        var binder = interactPlayer.GetComponent<CharacterBinder>();
        binder.ResetBindedCharacter();
        binder.BindCharacter(transform.parent.gameObject);
        GameManager.Instance.SetPlayerCharacter(selectedCharacter);
    }

    public override void HighLightOn()
    {
        base.HighLightOn();
        //them ui panel o day
    }

    public override void HighLightOff()
    {
        base.HighLightOff();
    }
}
