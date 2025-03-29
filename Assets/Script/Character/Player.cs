using System;
using System.Collections.Generic;
using Assets.Script;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody2D rd;
    public SpriteRenderer characterSR;
    private Animator anim;
    public bool isGamePaused = false;
    public int health;
    public int armor;
    public float speed;
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

    public BaseSkill Skill { get; private set; }

    //private Inventory inventory;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //menu= GameObject.Find("Menu");
        //menu.SetActive(false);

        health = playerDetailSO.playerHealthAmount;
        armor = playerDetailSO.playerArmorAmount;
        speed = playerDetailSO.playerSpeedAmount;

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        healthController = GetComponent<PlayerHealthController>();
        armorController = GetComponent<PlayerArmorController>();
        moveController = GetComponent<PlayerMoveController>();
        weaponController = GetComponentInChildren<WeaponController>();
        pickController = GetComponent<PlayerPickController>();
        Skill = GetComponentInChildren<BaseSkill>();

        if (Skill == null)
        {
            Debug.LogError("Skill Controller is missing on Player!");
        }
        //inventory = new Inventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !Skill.IsUsingSkill())
        {
            Debug.Log("Can CHange wweapon");
         //   weaponController.ChangeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !weaponController.haveOneWepon && !Skill.IsUsingSkill())
        {
            weaponController.ChangeWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.F) && pickController.isTouchItem && !Skill.IsUsingSkill())
        {
            pickController.PickItemWeapon();
        }

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (!isOpenBag)
        //    {
        //        isOpenBag = true;
        //        foreach (Transform child in transform)
        //        {
        //            child.gameObject.SetActive(false);
        //        }
        //        menu.SetActive(true);
        //    }
        //    else
        //    {
        //        isOpenBag = false;
        //        foreach (Transform child in transform)
        //        {
        //            child.gameObject.SetActive(true);
        //        }
        //        menu.SetActive(false);

        //    }
        //}

        
            if (!healthController.IsDead)
            {
                isMove = moveController.MoveControl();
                anim.SetBool("isMoving", isMove);
            }
            else
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
                weaponController.gameObject.SetActive(false);
                armorController.ArmorWhenPlayerDie();
                rd.linearVelocity = Vector2.zero;
                Cursor.visible = true;
                anim.SetBool("isDead", true);
            }
        
       

        if (healthController.IsHurt && !healthController.IsDead)
            anim.SetBool("isHurt", true);
        else
            anim.SetBool("isHurt", false);

        if (Input.GetKeyDown(KeyCode.E) && Skill.CanUseSkill1())
        {
            anim.SetBool("isSkill1", true);

        }
        if (Input.GetKeyDown(KeyCode.Q) && Skill.CanUseSkill2())
        {
           anim.SetBool("isSkill2", true);
           // Skill.UltimmateSkill();
        }
    }


}
