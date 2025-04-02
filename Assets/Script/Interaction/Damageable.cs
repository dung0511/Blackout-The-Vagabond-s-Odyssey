using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour, IDamageable
{
    public int health = 3;
    private Lootable lootable;

    private void Awake()
    {
        lootable = GetComponent<Lootable>();
    }

    public virtual void takeDame(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Break();
            
        }
    }

    private void Break()
    {
        AstarPath.active.Scan();
        if (lootable != null)
        {
            lootable.DropLoot();
        }
        Destroy(gameObject);
    }
}
