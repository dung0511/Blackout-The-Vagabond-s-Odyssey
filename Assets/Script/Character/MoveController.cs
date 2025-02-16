using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
//using static UnityEngine.GraphicsBuffer;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    private Vector3 moveInput;
    public Rigidbody2D rd;
    public SpriteRenderer characterSR;
    private Animator anim;
    public bool isGamePaused = false;
    public int health;
    public bool isHurt;
    public bool isDead;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = characterSR.GetComponentInChildren<Animator>();
        health=GetComponent<PlayerHealth>().health;
        isGamePaused = false;
       
        isHurt = GetComponent<PlayerHealth>().isHurt;
        isDead = GetComponent<PlayerHealth>().isDead;
        Debug.Log("sdgbjihdgb:"+anim.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveControl();
            CheckHeal();
        }
        else
        {
            rd.linearVelocity = Vector2.zero;
           // GetComponentInChildren<WeaponManagemnt>().enabled = false;
           
            transform.Find("Aim").gameObject.SetActive(false);
            transform.Find("WeaponManagemnt").gameObject.SetActive(false);
            //GetComponent<BoxCollider2D>().enabled = false;
            Cursor.visible = true;

        }
        //else
        //{
        //    anim.SetBool("isDead", true);
        //}
        if (isHurt && !isDead)
        {
            anim.SetBool("isHurt", true);
        }
        else anim.SetBool("isHurt", false);
        if (isDead) anim.SetBool("isDead", true);
    }

    private void MoveControl()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");


        if (moveInput.magnitude > 1)
        {
            moveInput = moveInput.normalized;
        }


        anim.SetBool("isMoving", moveInput.magnitude > 0.01f);

        rd.linearVelocity = moveInput * speed;
    }

    private void CheckHeal()
    {
        isHurt = GetComponent<PlayerHealth>().isHurt;
        isDead = GetComponent<PlayerHealth>().isDead;
        health = GetComponent<PlayerHealth>().health;
        
    }

    
}
