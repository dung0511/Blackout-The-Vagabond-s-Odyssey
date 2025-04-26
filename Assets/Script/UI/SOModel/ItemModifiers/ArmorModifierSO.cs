using UnityEngine;

[CreateAssetMenu]
public class ArmorModifierSO : StatsModifierSO
{
    public int percentIncrease;

    public override void AffectCharacter(GameObject player, float value)
    {
        if (player.TryGetComponent<PlayerArmorController>(out var armor))
        {
            armor.IncreaseMaxArmor(percentIncrease);
        }
    }

    
}
