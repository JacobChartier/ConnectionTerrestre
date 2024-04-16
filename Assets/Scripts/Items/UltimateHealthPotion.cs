using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_health_potion");

        Name = "Potion de Vie (Ultime)";
        Description = "Potion puissante qui régénère tout ton <color=#FF2E2E>HP</color>. Peut être utilisé en ou hors combat. Ne peut pas être acheté, est seulement laissé par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.POTION;

        IsBuyable = false;
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");

        player.GetComponent<Health>().AddHealthPoint(player.GetComponent<EntityStats>().Health.Max * 1f);
    }
}
