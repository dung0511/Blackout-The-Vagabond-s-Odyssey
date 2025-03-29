using UnityEngine;

public class PlayableCharacter : Interactable
{
    public CharacterVariantSO selectedCharacter;
    public GameObject characterInfoPanel;
    public GameObject characterSkillsPanel;

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
        UpdateAndShowPanels(true);
    }

    public override void HighLightOff()
    {
        base.HighLightOff();
        UpdateAndShowPanels(false);
    }
    private void UpdateAndShowPanels(bool show)
    {
        if (characterInfoPanel != null && characterSkillsPanel != null)
        {
            if (show)
            {

                var infoPanelScript = characterInfoPanel.GetComponent<CharacterInfoPanel>();
                if (infoPanelScript != null)
                {
                    infoPanelScript.UpdateCharacterInfo(selectedCharacter);
                }


                var skillsPanelScript = characterSkillsPanel.GetComponent<CharacterSkillsPanel>();
                if (skillsPanelScript != null)
                {
                    skillsPanelScript.UpdateCharacterSkills(selectedCharacter);
                }
            }

            characterInfoPanel.SetActive(show);
            characterSkillsPanel.SetActive(show);
        }
    }
}
