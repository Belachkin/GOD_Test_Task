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
    [SerializeField] private float detectionRadius = 2f;
    
    private EnemyStateMashine stateMashine;
    
    private void Start()
    {
        stateMashine = new EnemyStateMashine();

        stateMashine.AddState(new RandomWalkEnemyState(stateMashine, rb, walkSpeed, changeDirectionInterval, detectionRadius));
        stateMashine.AddState(new ChasingEnemyState(stateMashine, rb, walkSpeed, detectionRadius));
        
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
