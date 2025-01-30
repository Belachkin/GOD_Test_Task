using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enemy.States;
using UnityEngine;

public class EnemyStateMashine
{
    private Dictionary<Type, IEnemyState> states = new Dictionary<Type, IEnemyState>();
    private IEnemyState currentState;

    public void AddState(IEnemyState state)
    {
        states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : IEnemyState
    {
        var type = typeof(T);

        if (currentState != null && currentState.GetType() == type)
        {
            return;
        }

        if (states.TryGetValue(type, out var newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
    
    public void Update()
    {
        currentState.Updater();
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdater();
    }
    
    
}
