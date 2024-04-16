using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_warrior_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_warrior_essence");

        Name = "Essence de Guerrier";
        Description = "Les attaques physiques deviennent 30% plus fortes. Ne peux pas être utilisé hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;

        IsUsableOnlyInCombat = true;

        Price = GeneratePrice(10, 17);
    }

    public override void Use()
    {
        Debug.LogWarning($"Item use has not been implemented.", this);
        Destroy(this.gameObject);
    }
}
