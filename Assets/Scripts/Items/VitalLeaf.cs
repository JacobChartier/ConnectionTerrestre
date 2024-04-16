using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalLeaf : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/vital_leaf");
        Model = Resources.Load<Mesh>("Meshes/Items/vital_leaf");

        Name = "Feuille Vital";
        Description = "Si cette feuille magique se trouve dans ton inventaire quand tu meurs au combat, tu reviens en vie avec ¼ de ton <color=#FF2E2E>HP</color> maximal.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.LEAF;

        IsUsableOnlyInCombat = true;

        Price = GeneratePrice(18, 27);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().Health.Max * 0.25f;

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
