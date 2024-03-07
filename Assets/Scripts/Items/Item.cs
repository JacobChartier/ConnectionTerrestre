using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[Serializable]
public abstract class Item : MonoBehaviour, IGameItemBase
{
    public int ID = -1;

    [Header("Data")]
    public Sprite Icon;
    public string Name = "Invalid Item";
    public string Description;

    public Rarety Rarety { get; protected set; } = Rarety.INVALID;
    public Category Category { get; protected set; } = Category.INVALID;

    public bool isUsableOnlyInCombat { get; protected set; } = false;
    public bool isUsableOnlyInOverworld { get; protected set; } = false;

    protected void Start()
    {
        LoadAssets();
        GenerateData();
    }

    public abstract void Use();

    protected virtual void LoadAssets()
    {
        Icon = Resources.Load<Sprite>("Sprites/MissingTexture");
    }

    protected virtual void GenerateData()
    {

    }

    public string tooltipName;
    public string tooltipDescription;
    public int price;

    [SerializeField] private List<string> customFields = new List<string>();
    public bool isStackable = true;
    public int stackSize = 5;
    public bool isBreakable = false;
    public int maxDurability = 10;
    public int durability = 10;


    public int GeneratePrice(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public bool Purchase(EntityStats player)
    {
        if (price <= player.Coins.Current)
        {
            player.Coins.Remove(price);
            MenuHandler.Instance.GetMenu<InventoryUI>().Refresh();

            return true;
        }

        return false;
    }

    public string GenerateTooltipTitle()
    {
        string tooltip = $"<size=+5><b>{this.Name}";

        //switch (category)
        //{
        //    case Type.ULTIMATE_HEALTH_POTION:
        //    case Type.ULTIMATE_MAGIC_ESSENCE:
        //        tooltip += " (Ultimate)</b></size>";
        //        break;

        //    case Type.STRONG_HEALTH_POTION:
        //    case Type.STRONG_MAGIC_ESSENCE:
        //        tooltip += " (Strong)</b></size>";
        //        break;

        //    case Type.WEAK_HEALTH_POTION:
        //    case Type.WEAK_MAGIC_ESSENCE:
        //        tooltip += " (Weak)</b></size>";
        //        break;

        //    default:
        //        tooltip += "</b></size>";
        //        break;
        //}


        switch (Rarety)
        {
            case Rarety.LEGENDARY:
                tooltip += $"<br><color=#FFD700>LEGENDARY";
                break;

            case Rarety.EPIC:
                tooltip += $"<br><color=#F83BFF>EPIC";
                break;

            case Rarety.RARE:
                tooltip += $"<br><color=#384CFF>RARE";
                break;

            case Rarety.COMMON:
                tooltip += $"<br><color=#FFFFFF>COMMON";
                break;

            default:
                tooltip += "";
                break;
        }

        //switch (category)
        //{
        //    case Type.WEAK_HEALTH_POTION:
        //    case Type.NORMAL_HEALTH_POTION:
        //    case Type.STRONG_HEALTH_POTION:
        //    case Type.ULTIMATE_HEALTH_POTION:
        //    case Type.NORMAL_EXPERIENCE_POTION:
        //        tooltip += " POTION</color>";
        //        break;

        //    case Type.WEAK_MAGIC_ESSENCE:
        //    case Type.NORMAL_MAGIC_ESSENCE:
        //    case Type.STRONG_MAGIC_ESSENCE:
        //    case Type.ULTIMATE_MAGIC_ESSENCE:
        //    case Type.NORMAL_WARRIOR_ESSENCE:
        //    case Type.NORMAL_MAGICIAN_ESSENCE:
        //        tooltip += " ESSENCE</color>";
        //        break;

        //    case Type.SHIELD:
        //        tooltip += " SHIELD</color>";
        //        break;

        //    case Type.FOUR_LEAF_CLOVER:
        //        tooltip += " CLOVER</color>";
        //        break;

        //    case Type.VITAL_LEAF:
        //        tooltip += " LEAF</color>";
        //        break;

        //    default:
        //        tooltip += " ITEM</color>";
        //        break;
        //}

        return tooltip;
    }

    public string GenerateTooltipDescription()
    {
        string tooltip = this.tooltipDescription;

        // Custom fields

        if (customFields.Count > 0)
            foreach (var field in customFields)
                tooltip += $"<br>{field}<br>";

        // Buffs of the item

        //if (buffs.HEALTH.number > 0)
        //    tooltip += $"<br>+{buffs.HEALTH.number}{(buffs.HEALTH.isPercentage ? "%" : "")} <color=#FF2E2E>Health Points</color>";

        //if (buffs.PHYSICAL_DAMAGES.number > 0)
        //    tooltip += $"<br>+{buffs.PHYSICAL_DAMAGES.number}{(buffs.PHYSICAL_DAMAGES.isPercentage ? "%" : "")} <color=#FF2E2E>Physical Damages</color>";

        //if (buffs.MAGIC.number > 0)
        //    tooltip += $"<br>+{buffs.MAGIC.number}{(buffs.MAGIC.isPercentage ? "%" : "")} <color=#34BAEB>Magic Points</color>";

        //if (buffs.MAGICAL_DAMAGES.number > 0)
        //    tooltip += $"<br>+{buffs.MAGICAL_DAMAGES.number}{(buffs.MAGICAL_DAMAGES.isPercentage ? "%" : "")} <color=#34BAEB>Magical Damages</color>";

        //if (buffs.DEFENSE.number > 0)
        //    tooltip += $"<br>+{buffs.DEFENSE.number}{(buffs.DEFENSE.isPercentage ? "%" : "")} <color=#213BFF>Defense</color>";

        //if (buffs.STRENGTH.number > 0)
        //    tooltip += $"<br>+{buffs.STRENGTH.number}{(buffs.STRENGTH.isPercentage ? "%" : "")} <color=#FF0F0F>Strength</color>";

        //if (buffs.ATTACK_SPEED.number > 0)
        //    tooltip += $"<br>+{buffs.ATTACK_SPEED.number}{(buffs.ATTACK_SPEED.isPercentage ? "%" : "")} <color=#FCFF4A>Attack Speed</color>";

        //if (buffs.COINS.number > 0)
        //    tooltip += $"<br>+{buffs.COINS.number}{(buffs.COINS.isPercentage ? "%" : "")} <color=#FFD700>Coins</color>";

        //if (buffs.EXPERIENCE.number > 0)
        //    tooltip += $"<br>+{buffs.EXPERIENCE.number}{(buffs.EXPERIENCE.isPercentage ? "%" : "")} <color=#00D0FF>Experience</color>";

        // Durability

        if (isBreakable)
            tooltip += $"<br><br><color=#505050>Durability: {(durability < (maxDurability / 3) ? $"<color=#FF0000>{durability}</color>" : $"{durability}")}/{maxDurability}</color>";

        // Use Only In

        if (isUsableOnlyInCombat | isUsableOnlyInOverworld)
            tooltip += "<br>";

        if (isUsableOnlyInCombat)
            tooltip += "<br><color=#FF0F0F>Cannot be use in world.</color>";

        if (isUsableOnlyInOverworld)
            tooltip += "<br><color=#FF0F0F>Cannot be use in combat.</color>";

        return tooltip;
    }

    public void SetData(string name = null, Category category = Category.INVALID, Rarety rarety = Rarety.INVALID)
    {
        this.name = name;
        this.Rarety = rarety;
    }
}

public enum Category
{
    POTION,
    ESSENCE,
    SHIELD,
    LEAF,

    INVALID = -1,
    DEBUG = -2
}

public enum Rarety
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY,

    INVALID = -1,
    DEBUG = -2
}

