using Assets.Script.UI.Inventory;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rd;
    public SpriteRenderer characterSR;
    private Animator anim;
    public bool isGamePaused = false;
    public int health;
    public int armor;
    public bool isHurt;
    public bool isDead;
    public bool isMove;
    public bool isOpenBag = false;
    public PlayerDetailSO playerDetailSO;

    public GameObject menu;

    public PlayerHealthController healthController;
    public PlayerArmorController armorController;
    public PlayerMoveController moveController;
    public WeaponController weaponController;
    public PlayerPickController pickController;
    
    //quang_ui_inventory
    public InventoryObject inventory;

    private void Awake()
    {
        menu= GameObject.Find("Menu");
        menu.SetActive(false);
        health = playerDetailSO.playerHealthAmount;
        armor = playerDetailSO.playerArmorAmount;


        rd = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        healthController = GetComponent<PlayerHealthController>();
        armorController = GetComponent<PlayerArmorController>();
        moveController = GetComponent<PlayerMoveController>();
        weaponController = GetComponentInChildren<WeaponController>();
        pickController = GetComponent<PlayerPickController>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponController.ChangeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !weaponController.haveOneWepon)
        {
            weaponController.ChangeWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.F) && pickController.isTouchItem)
        {
            pickController.PickItemWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpenBag)
            {
                isOpenBag = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                menu.SetActive(true);
            }
            else
            {
                isOpenBag = false;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
                menu.SetActive(false);
                
            }
        }

        if (!healthController.IsDead)
        {
            isMove = moveController.MoveControl();
            anim.SetBool("isMoving", isMove);
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            armorController.ArmorWhenPlayerDie();
            rd.linearVelocity = Vector2.zero;
            Cursor.visible = true;
            anim.SetBool("isDead", true);
        }

        if (healthController.IsHurt && !healthController.IsDead)
            anim.SetBool("isHurt", true);
        else
            anim.SetBool("isHurt", false);
    }

    //quang_ui_inventory
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.addItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
