using UnityEngine;

public class ShieldHitBox : MonoBehaviour
{
    private int shieldDame;
    private void Start()
    {
        shieldDame = GetComponentInParent<KnightSkill>().shieldDame;
        Debug.Log(shieldDame);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.takeDame(shieldDame);
        }
    }


}
