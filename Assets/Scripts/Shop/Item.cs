using System.Drawing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    const int HP_RECU_POTION = 40;
    const int MP_RECU_ESSENCE = 40;
    const int POURCENTAGE_VIE_FEUILLE_VITALE = 25;
    const int POURCENTAGE_ESSENCE_GUERRIER = 30;
    const int POURCENTAGE_ESSENCE_MAGICIEN = 30;

    internal int itemID;

    [Header("Description")]
    public Sprite icon;
    
    public new string name;
    public string description;

    public string tooltipName;
    public string tooltipDescription;

    public Type category;
    public Rarety rarety;

    public int price;

    [Header("Options")]
    public bool isStackable;
    public int stackSize;

    public bool isBreakable = false;
    public int durability;

    public void FaireEffet(EntityStats stats)
    {
        switch (category)
        {
            case Type.NORMAL_HEALTH_POTION:
                stats.Health.Add(HP_RECU_POTION);
                break;

            case Type.WEAK_HEALTH_POTION:
                stats.Health.Add(HP_RECU_POTION / 2);
                break;

            case Type.STRONG_HEALTH_POTION:
                stats.Health.Add(HP_RECU_POTION * 2);
                break;

            case Type.ULTIMATE_HEALTH_POTION:
                stats.Health.Add(stats.Health.Max);
                break;

            case Type.WEAK_MAGIC_ESSENCE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE);
                break;

            case Type.NORMAL_MAGIC_ESSENCE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE / 2);
                break;

            case Type.STRONG_MAGIC_ESSENCE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE * 2);
                break;

            case Type.ULTIMATE_MAGIC_ESSENCE:
                stats.MagicPoint.Add(stats.MagicPoint.Max);
                break;

            case Type.VITAL_LEAF:
                stats.Health.Add(stats.Health.Max / (100 / POURCENTAGE_VIE_FEUILLE_VITALE));
                break;

            case Type.NORMAL_WARRIOR_ESSENCE:
                stats.Strength.Add(stats.Strength.Current * (1.0f + POURCENTAGE_ESSENCE_GUERRIER / 100.0f));
                break;

            case Type.NORMAL_MAGICIAN_ESSENCE:
                stats.Defense.Add(stats.Defense.Current * (1.0f + POURCENTAGE_ESSENCE_MAGICIEN / 100.0f));
                break;
        }
    }
}

public enum Rarety
{
    COMMON, 
    RARE, 
    EPIC, 
    LEGENDARY
}

public enum Type
{
    // Potions
    WEAK_HEALTH_POTION, 
    NORMAL_HEALTH_POTION,
    STRONG_HEALTH_POTION, 
    ULTIMATE_HEALTH_POTION, 

    NORMAL_EXPERIENCE_POTION,

    // Essences
    WEAK_MAGIC_ESSENCE, 
    NORMAL_MAGIC_ESSENCE, 
    STRONG_MAGIC_ESSENCE, 
    ULTIMATE_MAGIC_ESSENCE, 

    NORMAL_WARRIOR_ESSENCE,
    NORMAL_MAGICIAN_ESSENCE,

    // Plants
    VITAL_LEAF,
    FOUR_LEAF_CLOVER,   

    // Tools
    SHIELD,

    OTHER

}