using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Combat;

[RequireComponent(typeof(Health)), Serializable]
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
    public EntityStatistic Defense = new EntityStatistic() { Base = 3, Min = 0, Max = 100 };
    public EntityStatistic MagicPoint = new EntityStatistic() { Base = 10, Min = 0, Max = 10 };
    public EntityStatistic Strength = new EntityStatistic() { Base = 10, Min = 0, Max = 10 };
    public EntityStatistic AttackSpeed = new EntityStatistic() { Base = 10, Min = 0, Max = 10 };
    public int Experience = 0; // fucking stupide entitystatistics veux pas fucking fonctionner stupide fucking reste pris à 0 j'hais les struct c'est tellement fucking stupide laisse ca comme int je m'en fous fuck cette stupide structure à chier
    public int Niveau = 0;
    public List<InfoAttaque> Attaques = new List<InfoAttaque>();
    public EnemyType enemyType = EnemyType.DEFAULT; // seulement utilisé par ennemis

    [Header("Economy Statistics")]
    public int Coins = 0; // coins n'est pas float, alors pas un entity stat.

    //[Header("Enemy stats")]
    //private int exp_worth;
    //public int ExpWorth
    //{
    //    get => exp_worth;
    //    set
    //    {
    //        if (value < 0)
    //            value = 0;

    //        exp_worth = value;
    //    }
    //}

    private void OnEnable()
    {
        Health.Reset();
        Defense.Reset();
        MagicPoint.Reset();
        Strength.Reset();
        AttackSpeed.Reset();
        //Coins.Reset();

        if (gameObject.name == "Player")
        {
            DontDestroyOnLoad(this);
        }
    }

    // À NE SEULEMENT UTILISER QUAND UN ENEMI EST INITIALIZÉ!!!! NE JAMAIS UTILISER POUR JOUEUR OU APRÈS L'INITIALISATION DE L'ENNEMI
    // cette fonction existe car il n'est pas possible de remplacer une classe avec une autre, encore moins un monobehavior
    // les structs se copient par valeur et non par référence (struct my beloved)
    public void SetStats(EntityStats_struct stats)
    {
        Health.Current = stats.Health;
        MagicPoint.Current = stats.MagicPoint;
        Defense.Current = stats.Defense;
        AttackSpeed.Current = stats.AttackSpeed;
        Strength.Current = stats.Strength;
        Coins = stats.Coins;
        Experience = stats.Experience;
        Attaques.Clear();
        foreach (InfoAttaque a in stats.Attaques)
        {
            Attaques.Add(a);
        }
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
        set
        {
            if (value > Max) _current = Max;
            if (value < Min) _current = Min;

            else _current = value;
        }
    }

    [SerializeField] private float _multiplier;
    [HideInInspector] public float Multiplier
    {
        get => _multiplier;
        set
        {
            _multiplier = value;
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

    public bool Add(float amount)// ne fonctionne pas :)
    {
        if ((Current + (amount * Multiplier)) <= Max)
        {
            Current += (amount * Multiplier);
            return true;
        }
        else
        {
            Current = Max;
            return false;
        }
    }

    public bool Remove(float amount)
    {
        if ((Current - amount) >= Min)
        {
            Current -= amount;
            return true;
        }
        else
        {
            Current = Min;
            return false;
        }
    }

    public void Reset()
    {
        Current = Base;
    }
}

public struct EntityStats_struct
{
    public float Health;
    public float MagicPoint;
    public float Defense;
    public float Strength;
    public float AttackSpeed;
    public int Experience;
    public int Coins;
    public List<InfoAttaque> Attaques;

    public EntityStats_struct(float health, float magic, float defense, float strength, float speed, int exp, int coins, List<InfoAttaque> attaques)
    {
        this.Health = health;
        this.MagicPoint = magic;
        this.Defense = defense;
        this.AttackSpeed = speed;
        this.Strength = strength;
        this.Experience = exp;
        this.Coins = coins;
        this.Attaques = attaques;
    }
}