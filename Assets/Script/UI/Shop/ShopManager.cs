using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //singleton
    public static ShopManager Instance;

    public TextMeshProUGUI coinsTxt;

    // price va so luong da mua
    private int[] prices = { 10, 15, 30, 300, 320, 120 };
    private int[] quantities = new int[10];

    public GameObject[] itemPrefabs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DataManager.Load(); // ???
            UpdateCoinsUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCoinData()
    {
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI()
    {
        coinsTxt.text = "Coins: " + DataManager.gameData.playerData.coin;
    }

    public int GetPrice(int itemID) => prices[itemID - 1];
    public int GetQuantity(int itemID) => quantities[itemID - 1];

    public void BuyItem(int itemID)
    {
        if (DataManager.gameData.playerData.coin >= prices[itemID - 1])
        {
            //-coin +quantity
            DataManager.gameData.playerData.coin -= prices[itemID - 1];
            quantities[itemID - 1]++;

            DataManager.Save();

            UpdateCoinsUI();
            UpdateButtonQuantity(itemID); 

            SpawnItem(itemID);
        }
    }

    //update ui cho button trong shop
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

    //save coins vao data
    public void AddCoins(int amount)
    {
        DataManager.gameData.playerData.coin += amount;
        UpdateCoinsUI();
        DataManager.Save(); //??? save moi lan nhat do
    }

    private void SpawnItem(int itemID)
    {
        if (itemID < 1 || itemID > itemPrefabs.Length) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.Log("cant find player");

        //spawn item
        Vector3 spawnPos = player.transform.position +
                  player.transform.forward * 0.3f; 
        Instantiate(itemPrefabs[itemID - 1], spawnPos, Quaternion.identity);

        Debug.Log($"spawn item {itemID}");
    }
}