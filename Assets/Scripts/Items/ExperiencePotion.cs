using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ExperiencePotion : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_experience_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_experience_potion");

        Name = "Experience Potion";
        Description = "Augmente de 30% le montant d'experience recu après un combat. L'effet disparaît après 5 utilisations.";

        Rarety = Rarety.RARE;
        Category = Category.POTION;

        IsBreakable = true;
    }

    public override void Use()
    {
        Debug.LogWarning($"Item use has not been implemented.", this);
        Destroy(this.gameObject);
    }
}
