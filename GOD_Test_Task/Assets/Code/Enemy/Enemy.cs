using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy.States;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float travelDistance = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float changeDirectionInterval = 0.5f;
    
    private EnemyStateMashine stateMashine;
    
    private void Start()
    {
        stateMashine = new EnemyStateMashine();

        stateMashine.AddState(new RandomWalkEnemyState(stateMashine, rb, walkSpeed, travelDistance, changeDirectionInterval));
        
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
