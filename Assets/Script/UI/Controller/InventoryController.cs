using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventory inventoryUI;

    public int inventorySize = 10;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
