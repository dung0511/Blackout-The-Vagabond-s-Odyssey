using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSkillsPanel : MonoBehaviour
{
    public Image skillIcon1;
    public TextMeshProUGUI skillDescription1;
    public Image skillIcon2;
    public TextMeshProUGUI skillDescription2;

    public void UpdateCharacterSkills(CharacterVariantSO characterData)
    {
        if (characterData != null)
        {
            skillIcon1.sprite = characterData.characterSkillIcon1;
            skillDescription1.text = characterData.characterSkillDescription1;
            skillIcon2.sprite = characterData.characterSkillIcon2;
            skillDescription2.text = characterData.characterSkillDescription2;
        }
    }
}
