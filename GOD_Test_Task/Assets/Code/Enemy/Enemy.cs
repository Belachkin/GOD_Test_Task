using System;
using System.Collections.Generic;
using Code.Enemy.States;
using Code.Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float changeDirectionInterval = 0.5f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRadius = 2f;
    
    [SerializeField] private int damage = 1;
    [SerializeField] private int attackCooldown = 1;
    
    [SerializeField] private Health health;
    [SerializeField] private List<GameObject> dropItems;
    
    private EnemyStateMashine stateMashine;

    
    
    private void Start()
    {
        stateMashine = new EnemyStateMashine();

        stateMashine.AddState(new RandomWalkEnemyState(stateMashine, rb, 
                                                        walkSpeed, changeDirectionInterval, 
                                                        detectionRadius, animator, health));
        stateMashine.AddState(new ChasingEnemyState(stateMashine, rb, 
                                                    walkSpeed, detectionRadius, 
                                                    attackRadius, animator, health));
        stateMashine.AddState(new AttackEnemyState(stateMashine, rb, 
                                                    attackRadius, damage, 
                                                    attackCooldown, animator, health));
        
        stateMashine.AddState(new DeadEnemyState(stateMashine, animator));
        
        stateMashine.SetState<RandomWalkEnemyState>();
    }

    private void Update()
    {
        stateMashine.Update();
    }

    private void FixedUpdate()
    {
        stateMashine.FixedUpdate();
    }
    
    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        var newItem = Instantiate(dropItems[Random.Range(0, dropItems.Count)], transform.position, Quaternion.identity);
        newItem.GetComponent<Item>().Quantity = Random.Range(1, 3);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        
    }
}
