using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Health))]
public class EntityStats : MonoBehaviour
{
    public enum StatType
    {
        HP,
        MP,
        DEFENSE,
        FORCE,
        VITESSE,
        MAX_HP,
        MAX_MP,
        MAX_DEFENSE,
        MAX_FORCE,
        MAX_VITESSE
    }

    [Header("Combat Statistics")]
    public EntityStatistic Health = new EntityStatistic() { Base = 80, Min = 0, Max = 100 };
    public EntityStatistic Defense = new EntityStatistic() { Base = 10, Min = 0, Max = 100 };
    public EntityStatistic MagicPoint = new EntityStatistic() { Base = 10, Min = 0 };
    public EntityStatistic Strength = new EntityStatistic() { Base = 10, Min = 0 };
    public EntityStatistic AttackSpeed = new EntityStatistic() { Base = 10, Min = 0 };

    [Header("Economy Statistics")]
    public EntityStatistic Coins = new EntityStatistic() { Base = 10, Min = 0 };

    private void OnEnable()
    {
        Health.Reset();
        Defense.Reset();
        MagicPoint.Reset();
        Strength.Reset();
        AttackSpeed.Reset();
        Coins.Reset();
    }
}

[Serializable]
public struct EntityStatistic
{
    [SerializeField] private float _base;
    [HideInInspector] public float Base
    {
        get => _base;
        set => _base = value;
    }

    [SerializeField] private float _current;
    [HideInInspector] public float Current
    {
        get => _current;
        private set
        {
            if (value > Max) _current = Max;
            if (value < Min) _current = Min;

            else _current = value;
        }
    }

    [Space]
    [SerializeField] private float _max;
    [HideInInspector] public float Max
    {
        get => _max;
        set => _max = value;
    }

    [SerializeField] private float _min;
    [HideInInspector] public float Min
    {
        get => _min;
        set => _min = value;
    }

    [Space]
    [SerializeField] private string _displayColor;
    [HideInInspector] public string DisplayColor
    {
        get => _displayColor;
        set => _displayColor = value;
    }

    public bool Add(float amount)
    {
        if ((Current + amount) <= Max)
        {
            Current += amount;
            return true;
        }
        else return false;
    }

    public bool Remove(float amount)
    {
        if ((Current + amount) >= Min)
        {
            Current -= amount;
            return true;
        }
        else return false;
    }

    public void Reset()
    {
        Current = Base;
    }
}
