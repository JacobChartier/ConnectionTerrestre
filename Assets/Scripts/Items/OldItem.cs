using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class OldItem : ScriptableObject
{
    const int HP_RECU_POTION = 30;
    const int MP_RECU_ESSENCE = 30;
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
    public int price;

    [Header("Tooltip")]
    public Type category;
    public Rarety rarety;

    [Space]
    [SerializeField] private List<string> customFields = new List<string>();

    [Space]
    public Buffs buffs;

    [Header("Options")]

    [Space]
    public bool UseOnlyInCombat = false;
    public bool UseOnlyInWorld = false;

    [Space]
    public bool isStackable = true;
    public int stackSize = 5;

    [Space]
    public bool isBreakable = false;
    public int maxDurability = 10;
    public int durability = 10;

    //public int GeneratePrice(int min, int max)
    //{
    //    return UnityEngine.Random.Range(min, max);
    //}

    //public bool Purchase(EntityStats player)
    //{
    //    if (price <= player.Coins.Current)
    //    {
    //        player.Coins.Remove(price);

    //        return true;
    //    }

    //    return false;
    //}

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

            case Type.NORMAL_EXPERIENCE_POTION:
                break;
        }
    }

//    public string GenerateTooltipTitle()
//    {
//        string tooltip = $"<size=+5><b>{this.name}";

//        switch (category)
//        {
//            case Type.ULTIMATE_HEALTH_POTION:
//            case Type.ULTIMATE_MAGIC_ESSENCE:
//                tooltip += " (Ultimate)</b></size>";
//                break;

//            case Type.STRONG_HEALTH_POTION:
//            case Type.STRONG_MAGIC_ESSENCE:
//                tooltip += " (Strong)</b></size>";
//                break;

//            case Type.WEAK_HEALTH_POTION:
//            case Type.WEAK_MAGIC_ESSENCE:
//                tooltip += " (Weak)</b></size>";
//                break;

//            default:
//                tooltip += "</b></size>";
//                break;
//        }


//        switch (rarety)
//        {
//            case Rarety.LEGENDARY:
//                tooltip += $"<br><color=#FFD700>LEGENDARY";
//                break;

//            case Rarety.EPIC:
//                tooltip += $"<br><color=#F83BFF>EPIC";
//                break;

//            case Rarety.RARE:
//                tooltip += $"<br><color=#384CFF>RARE";
//                break;

//            case Rarety.COMMON:
//                tooltip += $"<br><color=#FFFFFF>COMMON";
//                break;

//            default:
//                tooltip += "";
//                break;
//        }

//        switch (category)
//        {
//            case Type.WEAK_HEALTH_POTION:
//            case Type.NORMAL_HEALTH_POTION:
//            case Type.STRONG_HEALTH_POTION:
//            case Type.ULTIMATE_HEALTH_POTION:
//            case Type.NORMAL_EXPERIENCE_POTION:
//                tooltip += " POTION</color>";
//                break;

//            case Type.WEAK_MAGIC_ESSENCE:
//            case Type.NORMAL_MAGIC_ESSENCE:
//            case Type.STRONG_MAGIC_ESSENCE:
//            case Type.ULTIMATE_MAGIC_ESSENCE:
//            case Type.NORMAL_WARRIOR_ESSENCE:
//            case Type.NORMAL_MAGICIAN_ESSENCE:
//                tooltip += " ESSENCE</color>";
//                break;

//            case Type.SHIELD:
//                tooltip += " SHIELD</color>";
//                break;

//            case Type.FOUR_LEAF_CLOVER:
//                tooltip += " CLOVER</color>";
//                break;

//            case Type.VITAL_LEAF:
//                tooltip += " LEAF</color>";
//                break;

//            default:
//                tooltip += " ITEM</color>";
//                break;
//        }

//        return tooltip;
//    }

//    public string GenerateTooltipDescription()
//    {
//        string tooltip = this.tooltipDescription;

//        // Custom fields

//        if (customFields.Count > 0)
//            foreach (var field in customFields)
//                tooltip += $"<br>{field}<br>";

//        // Buffs of the item

//        if (buffs.HEALTH.number > 0)
//            tooltip += $"<br>+{buffs.HEALTH.number}{(buffs.HEALTH.isPercentage ? "%" : "")} <color=#FF2E2E>Health Points</color>";

//        if (buffs.PHYSICAL_DAMAGES.number > 0)
//            tooltip += $"<br>+{buffs.PHYSICAL_DAMAGES.number}{(buffs.PHYSICAL_DAMAGES.isPercentage ? "%" : "")} <color=#FF2E2E>Physical Damages</color>";

//        if (buffs.MAGIC.number > 0)
//            tooltip += $"<br>+{buffs.MAGIC.number}{(buffs.MAGIC.isPercentage ? "%" : "")} <color=#34BAEB>Magic Points</color>";

//        if (buffs.MAGICAL_DAMAGES.number > 0)
//            tooltip += $"<br>+{buffs.MAGICAL_DAMAGES.number}{(buffs.MAGICAL_DAMAGES.isPercentage ? "%" : "")} <color=#34BAEB>Magical Damages</color>";

//        if (buffs.DEFENSE.number > 0)
//            tooltip += $"<br>+{buffs.DEFENSE.number}{(buffs.DEFENSE.isPercentage ? "%" : "")} <color=#213BFF>Defense</color>";

//        if (buffs.STRENGTH.number > 0)
//            tooltip += $"<br>+{buffs.STRENGTH.number}{(buffs.STRENGTH.isPercentage ? "%" : "")} <color=#FF0F0F>Strength</color>";

//        if (buffs.ATTACK_SPEED.number > 0)
//            tooltip += $"<br>+{buffs.ATTACK_SPEED.number}{(buffs.ATTACK_SPEED.isPercentage ? "%" : "")} <color=#FCFF4A>Attack Speed</color>";

//        if (buffs.COINS.number > 0)
//            tooltip += $"<br>+{buffs.COINS.number}{(buffs.COINS.isPercentage ? "%" : "")} <color=#FFD700>Coins</color>";

//        if (buffs.EXPERIENCE.number > 0)
//            tooltip += $"<br>+{buffs.EXPERIENCE.number}{(buffs.EXPERIENCE.isPercentage ? "%" : "")} <color=#00D0FF>Experience</color>";

//        // Durability

//        if (isBreakable)
//            tooltip += $"<br><br><color=#505050>Durability: {(durability < (maxDurability / 3) ? $"<color=#FF0000>{durability}</color>" : $"{durability}")}/{maxDurability}</color>";

//        // Use Only In

//        if (UseOnlyInCombat | UseOnlyInWorld)
//            tooltip += "<br>";

//        if (UseOnlyInCombat)
//            tooltip += "<br><color=#FF0F0F>Cannot be use in world.</color>";

//        if (UseOnlyInWorld)
//            tooltip += "<br><color=#FF0F0F>Cannot be use in combat.</color>";

//        return tooltip;
//    }
}

//public enum Rarety
//{
//    COMMON,
//    RARE,
//    EPIC,
//    LEGENDARY
//}

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

[Serializable]
public struct Buff
{
    public int number;
    public bool isPercentage;
}

[Serializable]
public struct Buffs
{
    public Buff HEALTH;
    public Buff PHYSICAL_DAMAGES;
    public Buff MAGIC;
    public Buff MAGICAL_DAMAGES;
    public Buff DEFENSE;
    public Buff STRENGTH;
    public Buff ATTACK_SPEED;
    public Buff COINS;
    public Buff EXPERIENCE;
}