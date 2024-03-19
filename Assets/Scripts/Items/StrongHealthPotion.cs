using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/strong_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/strong_health_potion");

        Name = "Health Potion (Strong)";
        Description = "Potion forte qui régénère 60% de ton HP maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.POTION;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
