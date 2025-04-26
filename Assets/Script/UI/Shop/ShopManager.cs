using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public TextMeshProUGUI coinsTxt;

    public GameObject[] weaponPrefabs;
    public GameObject[] potionPrefabs;

    public ButtonInfo[] buttonSlots; // 8 slots: 0–3 weapons, 4–7 potions

    private int[] prices = new int[8];
    private int[] quantities = new int[8];
    private int playerCoins = 0; // test

    private GameObject[] itemPrefabs = new GameObject[8];


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetShop(); 
    }

    void Start()
    {
        InitShopItems();
        UpdateCoinsUI();
    }

    public void InitShopItems()
    {
        var selectedWeapons = GetRandomPrefabs(weaponPrefabs, 4);
        var selectedPotions = GetRandomPrefabs(potionPrefabs, 4);

        for (int i = 0; i < prices.Length; i++)
        {
            prices[i] = Random.Range(10, 201);
        }

        // Weapon slots (0-3)
        for (int i = 0; i < 4; i++)
        {
            int itemID = i + 1;
            buttonSlots[i].SetupSlot(selectedWeapons[i], itemID, prices[itemID - 1]);
            itemPrefabs[itemID - 1] = selectedWeapons[i];
        }

        // Potion slots (4-7)
        for (int i = 0; i < 4; i++)
        {
            int itemID = i + 5;
            buttonSlots[i + 4].SetupSlot(selectedPotions[i], itemID, prices[itemID - 1]);
            itemPrefabs[itemID - 1] = selectedPotions[i];
        }
    }


    private GameObject[] GetRandomPrefabs(GameObject[] sourcePrefabs, int count)
    {
        List<GameObject> sourceList = new List<GameObject>(sourcePrefabs);
        List<GameObject> selected = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            if (sourceList.Count == 0) break;

            int randIndex = Random.Range(0, sourceList.Count);
            selected.Add(sourceList[randIndex]);
            sourceList.RemoveAt(randIndex); 
        }

        return selected.ToArray();
    }


    public void UpdateCoinsUI()
    {
        coinsTxt.text = playerCoins.ToString();
    }

    public int GetPrice(int itemID) => prices[itemID - 1];
    public int GetQuantity(int itemID) => quantities[itemID - 1];

    public void BuyItem(int itemID)
    {
        if (playerCoins >= prices[itemID - 1] && quantities[itemID - 1] > 0)
        {
            playerCoins -= prices[itemID - 1];
            quantities[itemID - 1]--; 
            UpdateCoinsUI();
            UpdateButtonQuantity(itemID);
            SpawnItem(itemID);
        }

    }

    public void UpdateButtonQuantity(int itemID)
    {
        foreach (var button in buttonSlots)
        {
            if (button.itemID == itemID)
            {
                button.UpdateUI();
                break;
            }
        }
    }

    public void SpawnItem(int itemID)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Không tìm thấy Player");
            return;
        }

        Vector3 spawnPos = player.transform.position + player.transform.forward * 0.5f;
        spawnPos.y -= 1.5f;
        Instantiate(itemPrefabs[itemID - 1], spawnPos, Quaternion.identity);
    }

    public void ResetCoins()
    {
        playerCoins = 0;
        UpdateCoinsUI();
    }

    public void ResetShop()
    {
        quantities = new int[8]; 

        for (int i = 0; i < 4; i++)
        {
            quantities[i] = 1; // Weapons
        }
        for (int i = 4; i < 8; i++)
        {
            quantities[i] = Random.Range(1, 6); // Potions
        }
        InitShopItems();
        UpdateAllButtonQuantities();
    }

    public void AddCoins(int randomValue)
    {
        playerCoins += randomValue;
        UpdateCoinsUI();
    }

    public void UpdateAllButtonQuantities()
    {
        foreach (var button in buttonSlots)
        {
            if (button != null)
            {
                button.UpdateUI(); 
            }
        }
    }

}
