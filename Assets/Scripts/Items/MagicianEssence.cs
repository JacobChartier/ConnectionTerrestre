using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_magician_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_magician_essence");

        Name = "Essence de Magicien";
        Description = "Les attaques magiques deviennent 30% plus fortes. Ne peux pas être utilisé hors combat.";

        Rarety = Rarety.EPIC;
        Category = Category.ESSENCE;

        IsUsableOnlyInCombat = true;

        Price = GeneratePrice(10, 17);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = 0.30f;

        player.GetComponent<EntityStats>().Defense.Multiplier += value;

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
