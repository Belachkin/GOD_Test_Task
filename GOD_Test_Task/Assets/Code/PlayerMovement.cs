using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    
    [SerializeField] private GameObject _player;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movement.x = _joystick.Horizontal;
        movement.y = _joystick.Vertical;
        
        if (movement.x != 0 || movement.y != 0)
        {
            // Вычисляем угол поворота
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            // Поворачиваем персонажа к новому углу
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // Плавно поворачиваем персонажа
            _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime); 
        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
