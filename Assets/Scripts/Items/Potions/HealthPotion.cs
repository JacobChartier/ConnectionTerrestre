using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthPotion : Item
{
    private Sprite[] icons = new Sprite[4];
    private Type type;

    public HealthPotion()
    {
        System.Random rng = new System.Random();
        Rarety = (Rarety)rng.Next(0, 4);

        Category = Category.POTION;
    }

    public override void Use()
    {
        Refresh();
    }

    protected override void Load()
    {
        icons[0] = Resources.Load<Sprite>("Sprites/Items/weak_health_potion");
        icons[1] = Resources.Load<Sprite>("Sprites/Items/normal_health_potion");
        icons[2] = Resources.Load<Sprite>("Sprites/Items/strong_health_potion");
        icons[3] = Resources.Load<Sprite>("Sprites/Items/ultimate_health_potion");
    }

    protected override void GenerateData()
    {
        SelectRarety();

        Name = $"Health Potion{(type == Type.NORMAL ? $"" : $" ({type.ToString().ToLower()})")}";
        Description = $"This is a health potion !";

        isBreakable = true;
        MaxDurability = UnityEngine.Random.Range(10, 100);
    }


    protected override void Refresh()
    {
        Debug.Log("REFRESH() -> From item");

        base.Refresh();
    }

    private void SelectRarety()
    {
        switch(Rarety)
        {
            case Rarety.COMMON:
                Icon = icons[0];
                type = Type.WEAK;
                break;

            case Rarety.RARE:
                Icon = icons[1];
                type = Type.NORMAL;
                break;

            case Rarety.EPIC:
                Icon = icons[2];
                type = Type.STRONG;
                break;

            case Rarety.LEGENDARY:
                Icon = icons[3];
                type = Type.ULTIMATE;
                break;

            default: 
                Icon = icons[0]; break; 
        }

    }

    private enum Type
    {
        WEAK,
        NORMAL,
        STRONG,
        ULTIMATE
    }
}
