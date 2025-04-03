using UnityEngine;

[CreateAssetMenu]
public class MaxHpModifierSO : StatsModifierSO
{
    public override void AffectCharcater(GameObject player, float value)
    {
        PlayerHealthController playerHp = player.GetComponent<PlayerHealthController>();
        if (playerHp != null)
        {
            playerHp.IncreaseMaxHP((int)value);
        }
    }

    
}
