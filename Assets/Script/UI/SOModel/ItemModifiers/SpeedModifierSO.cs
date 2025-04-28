using UnityEngine;

[CreateAssetMenu]
public class SpeedModifierSO : StatsModifierSO
{
    private float duration = 20f;

    public override void AffectCharacter(GameObject player, float value)
    {
        PlayerMoveController playerMove = player.GetComponent<PlayerMoveController>();
        if (playerMove != null)
        {
            playerMove.ApplySpeedBoost(value, duration);
        }
    }

    
}
