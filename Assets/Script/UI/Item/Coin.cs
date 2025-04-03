using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShopManager.Instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}