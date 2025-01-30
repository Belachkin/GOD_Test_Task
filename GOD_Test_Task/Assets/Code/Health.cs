using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthView healthView;
    [SerializeField] private int maxValue;
    private int value;
    
    public int Value => value;
    public int MaxValue => maxValue;
    private void Start()
    {
        value = maxValue;
    }

    public void TakeDamage(int damage)
    {
        value -= damage;
        HealthViewUpdate();
    }

    public void HealthViewUpdate()
    {
        Debug.Log($"{value}/{maxValue} = {value/maxValue}");
        healthView?.UpdateView();
    }
    
}
