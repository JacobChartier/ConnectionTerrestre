using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_magician_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_magician_essence");

        Name = "Magician Essence";
        Description = "Les attaques magiques deviennent 30% plus fortes. <br>Ne peux pas �tre utilis� hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;

        IsUsableOnlyInCombat = true;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
