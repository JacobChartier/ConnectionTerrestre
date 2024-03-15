using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_health_potion");

        Name = "Health Potion";
        Description = "Potion qui régénère 30% de ton HP maximal. <br>Peut être utilisé en ou hors combat.";

        Rarety = Rarety.RARE;
        Category = Category.POTION;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
