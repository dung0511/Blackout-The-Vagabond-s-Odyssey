using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoPanel : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI maxArmorText;
    public TextMeshProUGUI moveSpeedText;
    public void UpdateCharacterInfo(CharacterVariantSO characterData)
    {
        if (characterData != null)
        {
            characterImage.sprite = characterData.characterImage;
            characterNameText.text = characterData.characterName;
            maxHealthText.text =   characterData.maxHealth.ToString();
            maxArmorText.text =  characterData.maxArmor.ToString();
            moveSpeedText.text =  characterData.moveSpeed.ToString();
        }
    }
}
