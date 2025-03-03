using UnityEngine;

public class BreakableLootProp : Lootable, IDamageable
{
    [SerializeField] private int health = 3;

    public void takeDame(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DropLoot();
    }
}