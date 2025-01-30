using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy.States;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float changeDirectionInterval = 0.5f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRadius = 2f;
    
    [SerializeField] private int damage = 1;
    [SerializeField] private int attackCooldown = 1;
    
    private EnemyStateMashine stateMashine;
    
    private void Start()
    {
        stateMashine = new EnemyStateMashine();

        stateMashine.AddState(new RandomWalkEnemyState(stateMashine, rb, walkSpeed, changeDirectionInterval, detectionRadius));
        stateMashine.AddState(new ChasingEnemyState(stateMashine, rb, walkSpeed, detectionRadius, attackRadius));
        stateMashine.AddState(new AttackEnemyState(stateMashine, rb, attackRadius, damage, attackCooldown));
        
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
}
