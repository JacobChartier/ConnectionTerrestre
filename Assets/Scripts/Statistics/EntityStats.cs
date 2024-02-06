using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EntityStats : MonoBehaviour
{
    public float baseHP = 100, currentHP;
    public float baseDEF = 100, currentDEF;
    public float baseMP = 100, currentMP;
    public float baseStrength = 10, currentStrength;
    public float baseATKSpeed = 10, currentATKSpeed;

    public int coins = 0;

    public void Add(StatType type, float amount = 1)
    {
        switch (type)
        {
            case StatType.HP:
                currentHP += amount;
                break;

            case StatType.DEF:
                currentDEF += amount;
                break;

            case StatType.MP:
                currentMP += amount;
                break;

            case StatType.STRENGTH:
                currentStrength += amount;
                break;

            case StatType.ATK:
                currentATKSpeed += amount;
                break;

            case StatType.COINS:
                coins += (int)amount;
                break;
        }
    }

    public void Remove(StatType type, float amount = 1)
    {
        switch (type)
        {
            case StatType.HP:
                currentHP -= amount;
                break;

            case StatType.DEF:
                currentDEF -= amount;
                break;

            case StatType.MP:
                currentMP -= amount;
                break;

            case StatType.STRENGTH:
                currentStrength -= amount;
                break;

            case StatType.ATK:
                currentATKSpeed -= amount;
                break;

            case StatType.COINS:
                coins -= (int)amount;
                break;
        }
    }
}

public enum StatType
{
    HP, 
    DEF, 
    MP, 
    STRENGTH, 
    ATK, 
    COINS
}
