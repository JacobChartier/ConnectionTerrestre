using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateHealthPotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_health_potion");

        Name = "Health Potion (Ultimate)";
        Description = "Potion puissante qui régénère tout ton HP. Peut être utilisé en ou hors combat. Ne peut pas être acheté, est seulement laissé par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.POTION;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
