using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public TextMeshProUGUI coinsTxt;

    // price và số lượng đã mua
    private int[] prices = { 30, 45, 50, 300, 320, 120 };
    private int[] quantities = new int[10];
    private int playerCoins = 0; 

    public GameObject[] itemPrefabs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateCoinsUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCoinsUI()
    {
        coinsTxt.text = "Coins: " + playerCoins;
    }

    public int GetPrice(int itemID) => prices[itemID - 1];
    public int GetQuantity(int itemID) => quantities[itemID - 1];

    public void BuyItem(int itemID)
    {
        if (playerCoins >= prices[itemID - 1])
        {
            // - coin + quantity
            playerCoins -= prices[itemID - 1];
            quantities[itemID - 1]++;

            UpdateCoinsUI();
            UpdateButtonQuantity(itemID);
            SpawnItem(itemID);
        }
    }

    // update UI 
    public void UpdateButtonQuantity(int itemID)
    {
        ButtonInfo[] allButtons = FindObjectsOfType<ButtonInfo>();
        foreach (var button in allButtons)
        {
            if (button.itemID == itemID)
            {
                button.UpdateUI();
                break;
            }
        }
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        UpdateCoinsUI();
    }

    private void SpawnItem(int itemID)
    {
        if (itemID < 1 || itemID > itemPrefabs.Length) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Không tìm thấy player");
            return;
        }

        // Spawn item
        Vector3 spawnPos = player.transform.position + player.transform.forward * 0.3f;
        Instantiate(itemPrefabs[itemID - 1], spawnPos, Quaternion.identity);

        Debug.Log($"Đã spawn item {itemID}");
    }

    public void ResetCoins()
    {
        playerCoins = 0;
        UpdateCoinsUI();
    }
}