using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinCount;
    public ShopManager shopManager; 

    private int currentCoins = 0;

    private void Start()
    {
        coinCount = GameObject.Find("CoinsTxt")?.GetComponent<TextMeshProUGUI>();
        shopManager = FindObjectOfType<ShopManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("va cham Player!");
            currentCoins += 10;
            if (coinCount != null)
            {
                coinCount.text = currentCoins.ToString();
            }

            if (shopManager != null)
            {
                shopManager.coins += 10;
                shopManager.coinsTxt.text = "Coins: " + shopManager.coins.ToString();
            }

            Destroy(gameObject); 
        }
    }
}
