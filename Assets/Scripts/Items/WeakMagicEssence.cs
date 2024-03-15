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
        Description = "Essence faible qui régénère 15% de ton MP maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.ESSENCE;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
