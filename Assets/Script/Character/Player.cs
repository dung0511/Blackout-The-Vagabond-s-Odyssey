using System;
using System.Collections.Generic;
using Assets.Script;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //   public static Player Instance;

    public Rigidbody2D rd;
    public SpriteRenderer characterSR;
    private Animator anim;
    // public bool isGamePaused = false;
    public int health;
    public int armor;
    public float speed;
    public bool isHurt;
    public bool isDead;
    // public bool isMove;

    // public bool isOpenBag = false;

    public CharacterVariantSO characterSO;

    public PlayerHealthController healthController;
    public PlayerArmorController armorController;
    public PlayerMoveController moveController;
    public PlayerWeaponController weaponController;
    public PlayerPickController pickController;
    public InventoryController inventoryController;
    public BaseSkill skillController { get; private set; }

    private void Awake()
    {
        // Instance = this;
        DontDestroyOnLoad(gameObject);
        health = (int)characterSO.maxHealth;
        armor = (int)characterSO.maxArmor;
        speed = characterSO.moveSpeed;
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        healthController = GetComponent<PlayerHealthController>();
        armorController = GetComponent<PlayerArmorController>();
        moveController = GetComponent<PlayerMoveController>();
        weaponController = GetComponentInChildren<PlayerWeaponController>();
        pickController = GetComponent<PlayerPickController>();
        skillController = GetComponentInChildren<BaseSkill>();
        inventoryController = GetComponentInChildren<InventoryController>();

        if (skillController == null)
        {
            Debug.LogError("Skill Controller is missing on Player!");
        }

        if (ImageDisplay.INSTANCE != null)
        {
            ImageDisplay.INSTANCE.SetImage(characterSO.characterImage);
        }
        //inventory = new Inventory();
    }

    private void Update()
    {
        if (healthController.IsDead)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            weaponController.gameObject.SetActive(false);
            armorController.ArmorWhenPlayerDie();
            rd.linearVelocity = Vector2.zero;
            Cursor.visible = true;
            anim.SetBool("isDead", true);
        }
        else
        {
            anim.SetBool("isMoving", moveController.MoveControl());

            if (healthController.IsHurt && !healthController.IsDead)
                anim.SetBool("isHurt", true);
            else
                anim.SetBool("isHurt", false);

            if (!skillController.IsUsingSkill())
            {
                if (Input.GetButtonDown("weapon1"))
                {
                    weaponController.ChangeWeapon(1);
                }

                if (Input.GetButtonDown("weapon2") && !weaponController.haveOneWepon)
                {
                    weaponController.ChangeWeapon(2);
                }

                if (Input.GetButtonDown("Interact") && pickController.isTouchItem)
                {
                    pickController.PickItemWeapon();
                }

                weaponController.RotateWeapon();
                if (Input.GetButton("Fire"))
                {
                    weaponController.Attack();
                }
            }

            if (Input.GetButtonDown("bag") && !SettingsMenuUI.Instance.isOpenSettingMenu && !ShopUI.Instance.isOpenShop)
            {
                if (!inventoryController.isOpenInventory)
                {
                    UIManager.Instance.ToggleScreen(inventoryController);
                    AimPoint.Instance.DisableAim();
                    Cursor.visible = true;
                }
                else
                {

                    UIManager.Instance.ToggleScreen(inventoryController);
                    AimPoint.Instance.EnableAim();
                }
            }

            if (Input.GetButtonDown("Skill1") && skillController.CanUseSkill1())
            {
                anim.SetBool("isSkill1", true);

            }
            if (Input.GetButtonDown("Skill2") && skillController.CanUseSkill2())
            {
                anim.SetBool("isSkill2", true);
            }
        }



    }


}
