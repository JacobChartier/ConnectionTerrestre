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

        Name = "Potion d'Expérience";
        Description = "Augmente de 30% le montant d'<color=#91FF00>expérience</color> reçu après un combat. L'effet disparaît après 5 utilisations.";

        Rarety = Rarety.RARE;
        Category = Category.POTION;

        IsBreakable = true;

        Price = GeneratePrice(4, 9);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = 0.30f;

        RemainingUses--;
        InventoryLoader.Update(Player.Instance.inventory, this);

        switch (scene)
        {
            case Scenes.WORLD:
                UseInWorld(player, value);
                break;

            case Scenes.COMBAT:
                UseInCombat(player, value);
                break;
        }

        if (RemainingUses < 1)
        {
            InventoryLoader.Delete(Player.Instance.inventory, this);
            Destroy(this.gameObject);
        }
    }

    private void UseInWorld(GameObject player, float value)
    {
        if (player == null) return;

    }

    private void UseInCombat(GameObject player, float value)
    {
        if (player == null) return;

    }
}
