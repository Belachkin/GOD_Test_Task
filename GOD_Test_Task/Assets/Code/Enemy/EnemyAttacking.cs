using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private CircleCollider2D _collider;

    private bool isAttacking = false; // Флаг для отслеживания атаки

    public void AttackStart()
    {
        isAttacking = true; // Начало атаки
        _collider.gameObject.SetActive(true);
    }

    public void AttackEnd()
    {
        isAttacking = false; // Конец атаки
        _collider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Player"))
        {
            Debug.Log("DAMAGE");
            
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                
                // После нанесения урона сразу выключаем коллайдер
                _collider.gameObject.SetActive(false);
            }
        }
    }
    
}
