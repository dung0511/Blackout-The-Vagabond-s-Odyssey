using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.takeDame(GetComponentInParent<MeleeWeapon>().dame);
            Debug.Log("Melee Dealt:" + GetComponentInParent<MeleeWeapon>().dame);
        }
        else Debug.Log("Melee Dealt none because no enemy health");
    }
}
