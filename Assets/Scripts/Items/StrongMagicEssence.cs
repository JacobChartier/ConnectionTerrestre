using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/strong_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/strong_magic_essence");

        Name = "Magic Essence (Strong)";
        Description = "Essence forte qui régénère 60% de ton <color=#00FFFF>MP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
