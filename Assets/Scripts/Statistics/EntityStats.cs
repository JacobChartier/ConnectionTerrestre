using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EntityStats : MonoBehaviour
{
    public static float baseHP, currentHP;
    public static float baseDEF, currentDEF;
    public static float baseMP, currentMP;
    public static float baseStrengh, currentStrengh;
    public static float baseATKSpeed, currentATKSpeed;
    public static int coins = 0;

    public static void AddMP(float amount)
    {
        currentMP += amount;
    }

    public static void RemoveMP(float amount)
    {
        currentMP -= amount;
    }
}
