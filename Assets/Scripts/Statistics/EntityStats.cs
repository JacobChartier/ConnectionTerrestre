using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EntityStats : MonoBehaviour
{
    public static float baseHP = 100, currentHP;
    public static float baseDEF = 100, currentDEF;
    public static float baseMP = 100, currentMP;
    public static float baseStrength = 10, currentStrength;
    public static float baseATKSpeed = 10, currentATKSpeed;

    public static int coins = 0;

    public static void Add(StatType type, float amount = 1)
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

    public static void Remove(StatType type, float amount = 1)
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
