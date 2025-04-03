using UnityEngine;

[CreateAssetMenu]
public class ArmorModifierSO : StatsModifierSO
{
    public int percentIncrease = 20;

    public override void AffectCharcater(GameObject player, float value)
    {
        if (player.TryGetComponent<PlayerArmorController>(out var armor))
        {
            armor.IncreaseMaxArmor(percentIncrease);
        }
    }

    
}
