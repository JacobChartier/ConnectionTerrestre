using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeafClover : Item
{
    protected override void Load()
    {
        Id = 1;

        Icon = Resources.Load<Sprite>("Sprites/Items/four_leaf_clover");
        Model = Resources.Load<Mesh>("Meshes/Items/four_leaf_clover");

        Name = "Trèfle à Quatre Feuilles";
        Description = "Augmente de 50% la <color=#FFD700>monnaie</color> et l'<color=#91FF00>expérience</color> que le joueur reçoit lors de la défaite d’un ennemi. Cet item peut seulement être utilisé hors combat. L'effet dure 5 utilisations.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.LEAF;

        IsUsableOnlyInOverworld = true;

        IsBreakable = true;
        MaxUses = 5;

        Price = GeneratePrice(18, 27);
    }

    public override void Use()
    {
        RemainingUses--;

        if (RemainingUses < 1)
        {
            base.Use();
            Destroy(gameObject);
        }

    }
}
