using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3;
    private Lootable lootable;

    private void Awake()
    {
        lootable = GetComponent<Lootable>();
    }

    public void takeDame(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Break();
            AstarPath.active.Scan();
        }
    }

    private void Break()
    {
        if (lootable != null)
        {
            lootable.DropLoot();
        }
        Destroy(gameObject);
    }
}
