using UnityEngine;

public class ShieldHitBox : MonoBehaviour
{
    public int shieldDame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            AttackContext.CurrentAttackType = AttackType.NormalSkill;
            enemy.takeDame(shieldDame);
            Debug.Log("Dealt: "+ shieldDame);
        }
    }


}
