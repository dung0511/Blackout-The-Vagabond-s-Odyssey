using UnityEngine;

[CreateAssetMenu]
public class SpeedModifierSO : StatsModifierSO
{
    public float duration = 30f;
    public float percentBoost = 50f;

    public override void AffectCharcater(GameObject player, float value)
    {
        PlayerMoveController playerMove = player.GetComponent<PlayerMoveController>();
        if (playerMove != null)
        {
            playerMove.ApplySpeedBoost(percentBoost, duration);
        }
    }

    
}
