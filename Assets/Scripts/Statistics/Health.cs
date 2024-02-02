using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onGainHealth, onLostHealth, onDeath;

    public void AddHealthPoint(float HP)
    {
        EntityStats.currentHP += HP;

        onGainHealth.Invoke();
    }

    public void RemoveHealthPoint(float HP)
    {
        EntityStats.currentHP -= HP;

        onLostHealth.Invoke();

        if(EntityStats.currentHP <= 0)
        {
            onDeath.Invoke();
        }
    }
}
