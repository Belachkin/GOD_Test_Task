using System;
using System.Collections.Generic;
using Code.Inventory;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private List<SpriteRenderer> _flipSprites;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
        
        movement.x = _joystick.Horizontal;
        movement.y = _joystick.Vertical;

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (movement.x < 0)
        {
            for (int i = 0; i < _flipSprites.Count; i++)
            {
                _flipSprites[i].flipX = true;
            }
        }
        else if (movement.x > 0 && _flipSprites[0].flipX == true)
        {
            for (int i = 0; i < _flipSprites.Count; i++)
            {
                _flipSprites[i].flipX = false;
            }
        }
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime);
    }
}