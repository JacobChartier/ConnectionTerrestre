using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeakHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/weak_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/weak_health_potion");

        Name = "Health Potion (Weak)";
        Description = "Potion faible qui régénère 15% de ton HP maximal. <br>Peut être utilisé en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.POTION;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
