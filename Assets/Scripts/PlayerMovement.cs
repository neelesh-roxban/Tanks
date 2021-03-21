using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector3 moveDir;
    bool isPlayerOne;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        getInput();
        
    }

    void getInput()
    {

        float moveX = 0f;
        float moveY = 0f;


        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        rb.velocity = moveDir * speed;
    }
}
