using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/strong_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/strong_health_potion");

        Name = "Potion de Vie (Forte)";
        Description = "Potion forte qui régénère 60% de ton <color=#FF2E2E>HP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.POTION;

        Price = GeneratePrice(10, 17);
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");

        player.GetComponent<Health>().AddHealthPoint(player.GetComponent<EntityStats>().Health.Max * 0.60f);
    }
}
