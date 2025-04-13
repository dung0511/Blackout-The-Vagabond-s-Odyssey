using UnityEngine;

public class UltimateHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            AttackContext.CurrentAttackType = AttackType.UltimateSkill;
            enemy.takeDame(GetComponentInParent<AssassinSkill>().DameUltimateSkill);
        }
    }
}
