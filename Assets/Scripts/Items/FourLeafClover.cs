using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeafClover : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/four_leaf_clover");
        Model = Resources.Load<Mesh>("Meshes/Items/four_leaf_clover");

        Name = "Four Leaf Clover";
        Description = "Augmente de 50% la <color=#FFD700>monnaie</color> et expérience que le joueur reçoit lors de la défaite d’un ennemi. Cet item peut seulement être utilisé hors combat. L'effet dure 5 utilisations.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.LEAF;

        IsUsableOnlyInOverworld = true;

        IsBreakable = true;
        MaxUses = 5;

        RemainingUses = MaxUses;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
