using UnityEngine;

public class PlayableCharacter : Interactable
{
    public CharacterVariantSO selectedCharacter;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterBinder.Instance.ResetBindedCharacter();
        CharacterBinder.Instance.BindCharacter(transform.parent.gameObject);
        GameManager.Instance.SetPlayerCharacter(selectedCharacter);
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
