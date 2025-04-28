using UnityEngine;

[CreateAssetMenu]
public class ArmorModifierSO : StatsModifierSO
{
    public override void AffectCharacter(GameObject player, float value)
    {
        PlayerArmorController armor = player.GetComponent<PlayerArmorController>();
        if (armor != null)
        {
            armor.IncreaseMaxArmor((int)value);
        }
    }

    
}
