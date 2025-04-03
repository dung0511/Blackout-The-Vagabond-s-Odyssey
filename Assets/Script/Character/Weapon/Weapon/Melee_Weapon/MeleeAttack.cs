
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int Damege;

    private void Start()
    {
       // Damege=GetComponentInParent<MeleeWeapon>().meleeWeapon.GetDamage();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var prob))
        {
            prob.takeDame(Damege);
            Debug.Log("Dealt: " + Damege);
        }
    }
}
