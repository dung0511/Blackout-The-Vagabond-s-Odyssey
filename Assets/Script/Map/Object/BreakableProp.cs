using UnityEngine;

public class BreakableProp : MonoBehaviour, IDamageable
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

}