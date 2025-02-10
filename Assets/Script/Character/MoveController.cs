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
    
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = characterSR.GetComponent<Animator>();
       
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
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
}
