using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Shield : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/shield");
        Model = Resources.Load<Mesh>("Meshes/Items/shield");

        Name = "Shield";
        Description = "Si utilis� en combat, ta d�fense est augment�e pendant au moins 3 tours. Pour chaque tour apr�s le 3e en effet, le bouclier a 50% de chance de briser. <br><br>Si utilis� hors combat, pour un certain temps, si tu entre en combat, l�effet du bouclier sera automatiquement appliqu� sur toi.";

        Rarety = Rarety.EPIC;
        Category = Category.SHIELD;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
