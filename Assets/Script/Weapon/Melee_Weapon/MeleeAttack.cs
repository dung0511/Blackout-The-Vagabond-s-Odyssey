using System.Threading;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        //if (enemy != null)
        //{
           
        //    enemy.takeDame(GetComponentInParent<MeleeWeapon>().meleeWeapon._damage);
        //    Debug.Log("Melee Dealt:" + GetComponentInParent<MeleeWeapon>().dame);
        //}
        //else if(collision.gameObject.TryGetComponent<IDamageable>(out var prob))
        //{
        //    prob.takeDame(GetComponentInParent<MeleeWeapon>().dame);
        //}
    }
}
