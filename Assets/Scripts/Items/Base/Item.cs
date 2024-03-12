using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using static IItemBase;

[Serializable]
public abstract class Item : MonoBehaviour, IItemBase
{
    public int ID = -1;
    protected OnDataChange OnChange;

    public Sprite Icon;
    public Mesh Model;

    public string Name { get; set; } = $"Invalid Item";
    public string Description { get; set; } = $"";

    public Rarety Rarety { get; protected set; } = Rarety.INVALID;
    public Category Category { get; protected set; } = Category.INVALID;

    public bool IsUsableOnlyInCombat { get; protected set; } = false;
    public bool IsUsableOnlyInOverworld { get; protected set; } = false;

    public bool IsStackable { get; protected set; } = true;
    public int StackSize { get; protected set; } = 5;

    public bool IsBreakable { get; protected set; } = false;
    public int MaxUses { get; protected set; } = 10;
    public int Durability { get; set; } = 10;

    protected void Start()
    {
        Load();

        name = Name;

        // Check if the texture has loaded.
        if (Icon == null)
        {
            Debug.LogWarning($"Unable to load the texture from <b>{this.GetType().Name} ({typeof(Item)})</b>.", Icon);
            Icon = Resources.Load<Sprite>("Sprites/MissingTexture");
        }

        // Check is the mesh has loaded.
        if (Model == null)
        {
            Debug.LogWarning($"Unable to load the mesh from <b>{this.GetType().Name} ({typeof(Item)})</b>.", Model);
            Model = Resources.Load<Mesh>("Mesh/MissingMesh");
        }
    }

    public abstract void Use();

    protected virtual void Load()
    {

    }

    protected virtual void Refresh()
    {
        Debug.Log("REFRESH() -> From base");

        OnChange();
    }

    /* Everything below this will be reworked and might not be working in the future. DO NOT USE */
    [Header("Will be reworked")]
    
    // TODO: Change the tooltip generation system to only use one variable
    public string tooltipName;
    public string tooltipDescription;
    [SerializeField] private List<string> customFields = new List<string>();

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
        string tooltip = $"</color><br>{Description}";

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

        if (IsBreakable)
            tooltip += $"<br><br><color=#505050>Durability: {(Durability < (MaxUses / 3) ? $"<color=#FF0000>{Durability}</color>" : $"{Durability}")}/{MaxUses}</color>";

        // Use Only In

        if (IsUsableOnlyInCombat | IsUsableOnlyInOverworld)
            tooltip += "<br>";

        if (IsUsableOnlyInCombat)
            tooltip += "<br><color=#FF0F0F>Cannot be use in world.</color>";

        if (IsUsableOnlyInOverworld)
            tooltip += "<br><color=#FF0F0F>Cannot be use in combat.</color>";

        return tooltip;
    }

    // TODO: Make a new price generator
    public int price;

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

