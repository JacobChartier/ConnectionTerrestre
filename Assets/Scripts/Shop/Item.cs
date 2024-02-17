using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    const int HP_RECU_POTION = 40;
    const int MP_RECU_ESSENCE = 40;
    const int POURCENTAGE_VIE_FEUILLE_VITALE = 25;
    const int POURCENTAGE_ESSENCE_GUERRIER = 30;
    const int POURCENTAGE_ESSENCE_MAGICIEN = 30;

    [Header("Description")]
    public Sprite icon;
    
    public new string name;
    public string description;

    public ItemCategory category;
    public Rarety rarety;

    public int price;

    [Header("Options")]
    public bool isStackable;
    public int stackSize;

    public bool isBreakable = false;
    public int durability;

    public void FaireEffet(EntityStats stats)
    {
        switch (type)
        {
            case ItemCategory.POTION:
                stats.Health.Add(HP_RECU_POTION);
                break;

            case ItemCategory.POTION_FAIBLE:
                stats.Health.Add(HP_RECU_POTION / 2);
                break;

            case ItemCategory.POTION_FORTE:
                stats.Health.Add(HP_RECU_POTION * 2);
                break;

            case ItemCategory.POTION_ULTIME:
                stats.Health.Add(stats.Health.Max);
                break;

            case ItemCategory.ESSENCE_DE_VIE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE);
                break;

            case ItemCategory.ESSENCE_DE_VIE_FAIBLE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE / 2);
                break;

            case ItemCategory.ESSENCE_DE_VIE_FORTE:
                stats.MagicPoint.Add(MP_RECU_ESSENCE * 2);
                break;

            case ItemCategory.ESSENCE_DE_VIE_ULTIME:
                stats.MagicPoint.Add(stats.MagicPoint.Max);
                break;

            case ItemCategory.FEUILLE_VITALE:
                stats.Health.Add(stats.Health.Max / (100 / POURCENTAGE_VIE_FEUILLE_VITALE));
                break;

            case ItemCategory.ESSENCE_DE_GUERRIER:
                stats.Strength.Add(stats.Strength.Current * (1.0f + POURCENTAGE_ESSENCE_GUERRIER / 100.0f));
                break;

            case ItemCategory.ESSENCE_DE_MAGICIEN:
                stats.Defense.Add(stats.Defense.Current * (1.0f + POURCENTAGE_ESSENCE_MAGICIEN / 100.0f));
                break;
        }
    }
}

public enum ItemCategory
{
    potion,
    essence,
    shield
}