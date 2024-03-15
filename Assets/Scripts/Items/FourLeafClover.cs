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
        Description = "Augmente de 50% la <color=#FFD700>monnaie</color> et exp�rience que le joueur re�oit lors de la d�faite d�un ennemi. Cet item peut seulement �tre utilis� hors combat. L'effet dure 5 utilisations.";

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
