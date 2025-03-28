using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
//using static UnityEngine.GraphicsBuffer;

public class PlayerMoveController : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rd;

    private void Start()
    {
        speed = GetComponent<Player>().speed;
        rd = GetComponent<Rigidbody2D>();
    }


    public bool MoveControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontal, vertical);
        bool isMoving = moveInput.sqrMagnitude > 0.01f;


        rd.linearVelocity = moveInput.normalized * speed;

        return isMoving;
    }

}
