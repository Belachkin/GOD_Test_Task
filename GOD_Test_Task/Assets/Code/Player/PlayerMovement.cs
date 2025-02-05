using System;
using System.Collections.Generic;
using Code;
using Code.Inventory;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private List<SpriteRenderer> _flipSprites;
    
    [SerializeField] private Popup _deadPopupMenu;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Health health;
    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        
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

        if (health.Value <= 0 && !isDead)
        {
            animator.SetTrigger("Dead");
            isDead = true;
        }
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime);
    }

    public void Dead()
    {
        _deadPopupMenu.transform.GetChild(0).gameObject.SetActive(true);
        _deadPopupMenu.Show();
    }
}