using System.Threading;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private int Count=0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Count++;
        
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log(Count);
            enemy.takeDame(GetComponentInParent<MeleeWeapon>().dame);
            Debug.Log("Melee Dealt:" + GetComponentInParent<MeleeWeapon>().dame);
        }
        else if(collision.gameObject.TryGetComponent<IDamageable>(out var prob))
        {
            prob.takeDame(GetComponentInParent<MeleeWeapon>().dame);
        }
    }
}
