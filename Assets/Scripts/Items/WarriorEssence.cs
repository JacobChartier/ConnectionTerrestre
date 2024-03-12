using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_warrior_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_warrior_essence");

        Name = "Warrior Essence";
        Description = "Les attaques physiques deviennent 30% plus fortes. <br>Ne peux pas être utilisé hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;

        IsUsableOnlyInCombat = true;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
