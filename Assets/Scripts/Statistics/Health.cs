using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onGainHealth, onLostHealth, onDeath;
    private EntityStats stats;

    public void Awake()
    {
        stats = GetComponent<EntityStats>();
    }

    public void AddHealthPoint(float HP)
    {
        stats.Add(StatType.HP, HP);

        onGainHealth.Invoke();
    }

    public void RemoveHealthPoint(float HP)
    {
        stats.currentHP -= HP;

        onLostHealth.Invoke();

        if(stats.currentHP <= 0)
        {
            onDeath.Invoke();
        }
    }
}
