using UnityEngine;

[CreateAssetMenu]
public class SpeedModifierSO : StatsModifierSO
{
    public float duration = 30f;
    public float percentBoost;

    public override void AffectCharacter(GameObject player, float value)
    {
        PlayerMoveController playerMove = player.GetComponent<PlayerMoveController>();
        if (playerMove != null)
        {
            playerMove.ApplySpeedBoost(percentBoost, duration);
        }
    }

    
}
