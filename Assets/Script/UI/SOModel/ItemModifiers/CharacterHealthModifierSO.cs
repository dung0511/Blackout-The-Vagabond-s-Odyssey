using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifierSO : CharacterStatsModifierSO
{
    public override void AffectCharcater(GameObject player, float value)
    {
        PlayerHealthController playerHp = player.GetComponent<PlayerHealthController>();
        if (playerHp != null) 
            playerHp.RegenHealth((int)value);
    }
}
