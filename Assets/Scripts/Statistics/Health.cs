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

    public void AddHealthPoint(float amount)
    {
        stats.Health.Add(amount);

        onGainHealth.Invoke();
    }

    public void RemoveHealthPoint(float amount)
    {
        stats.Health.Remove(amount);

        onLostHealth.Invoke();

        if(stats.Health.Current == 0)
        {
            onDeath.Invoke();
        }
    }
}
