using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_magic_essence");

        Name = "Magic Essence";
        Description = "Essence qui r�g�n�re 30% de ton <color=#00FFFF>MP</color> maximal. Peut �tre utilis� en ou hors combat.";

        Rarety = Rarety.RARE;
        Category = Category.ESSENCE;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
