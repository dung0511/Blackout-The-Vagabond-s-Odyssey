using UnityEngine;

public class DashHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            AttackContext.CurrentAttackType = AttackType.NormalSkill;
            enemy.takeDame(GetComponentInParent<AssassinSkill>().DameNormalSkill);
        }
    }
}
