using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float horizontal;
    private float vertical;

    private Vector2 moveVector;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.pauseGame) return;

        HandleInputs();
        HandleScale();
    }
    
    void FixedUpdate()
    {
        if (GameManager.instance.pauseGame) return;
        // handling player movement
        rb.MovePosition(rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
    }
    
    
    void HandleInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveVector = new Vector2(horizontal, vertical);

    }

    void HandleScale()
    {
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}
