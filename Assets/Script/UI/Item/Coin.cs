using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int randomValue = Random.Range(5, 21); 
            ShopManager.Instance.AddCoins(randomValue);
            Destroy(gameObject);
        }
    }
}