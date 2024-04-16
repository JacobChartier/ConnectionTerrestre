using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ExperiencePotion : Item
{
    protected override void Load()
    {
        Id = 0;

        Icon = Resources.Load<Sprite>("Sprites/Items/normal_experience_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_experience_potion");

        Name = "Potion d'Exp�rience";
        Description = "Augmente de 30% le montant d'<color=#91FF00>exp�rience</color> re�u apr�s un combat. L'effet dispara�t apr�s 5 utilisations.";

        Rarety = Rarety.RARE;
        Category = Category.POTION;

        IsBreakable = true;

        Price = GeneratePrice(4, 9);
    }

    public override void Use()
    {
        Debug.LogWarning($"Item use has not been implemented.", this);
        Destroy(this.gameObject);
    }
}
