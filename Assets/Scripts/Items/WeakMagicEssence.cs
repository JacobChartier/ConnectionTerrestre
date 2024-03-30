using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeakMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/weak_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/weak_magic_essence");

        Name = "Magic Essence (Weak)";
        Description = "Essence faible qui régénère 15% de ton <color=#00FFFF>MP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.ESSENCE;
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");
        player.GetComponent<EntityStats>().MagicPoint.Add(player.GetComponent<EntityStats>().MagicPoint.Max * 0.15f);

        Destroy(this.gameObject);
    }
}
