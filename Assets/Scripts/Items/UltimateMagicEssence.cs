using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_magic_essence");

        Name = "Essence MAgique (Ultime)";
        Description = "Essence puissante qui régénère tout ton <color=#00FFFF>MP</color>. Peut être utilisé en ou hors combat. Ne peut pas être acheté, est seulement laissé par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.ESSENCE;

        IsBuyable = false;
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");
        player.GetComponent<EntityStats>().MagicPoint.Add(player.GetComponent<EntityStats>().MagicPoint.Max * 1.0f);

        Destroy(this.gameObject);
    }
}
