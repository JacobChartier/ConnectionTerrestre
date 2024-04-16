using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/strong_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/strong_magic_essence");

        Name = "Essence Magique (Forte)";
        Description = "Essence forte qui régénère 60% de ton <color=#00FFFF>MP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;

        Price = GeneratePrice(10, 17);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().MagicPoint.Max * 0.60f;

        player.GetComponent<EntityStats>().MagicPoint.Add(value);

        switch (scene)
        {
            case Scenes.WORLD:
                UseInWorld(player, value);
                break;

            case Scenes.COMBAT:
                UseInCombat(player, value);
                break;
        }

        InventoryLoader.Delete(Player.Instance.inventory, this);
        Destroy(this.gameObject);
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
