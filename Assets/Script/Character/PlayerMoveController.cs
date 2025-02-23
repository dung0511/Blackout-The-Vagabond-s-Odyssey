using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
//using static UnityEngine.GraphicsBuffer;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    private Vector3 moveInput;
    public Rigidbody2D rd;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    public bool MoveControl()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.magnitude > 1)
        {
            moveInput = moveInput.normalized;
        }
        rd.linearVelocity = moveInput * speed;
        return moveInput.magnitude > 0.01f;
    }

}
