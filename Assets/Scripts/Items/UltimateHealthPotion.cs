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
        Description = "Potion puissante qui r�g�n�re tout ton HP. Peut �tre utilis� en ou hors combat. Ne peut pas �tre achet�, est seulement laiss� par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.POTION;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
