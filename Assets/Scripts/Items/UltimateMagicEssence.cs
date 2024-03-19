using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_magic_essence");

        Name = "Magic Essence (Ultimate)";
        Description = "Essence puissante qui r�g�n�re tout ton MP. Peut �tre utilis� en ou hors combat. Ne peut pas �tre achet�, est seulement laiss� par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.ESSENCE;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
