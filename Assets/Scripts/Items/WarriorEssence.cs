using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_warrior_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_warrior_essence");

        Name = "Essence de Guerrier";
        Description = "Les attaques physiques deviennent 30% plus fortes. Ne peux pas être utilisé hors combat.";

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
                WorldBehaviour(player, value);
                break;

            case Scenes.COMBAT:
                CombatBehaviour(player, value);
                break;
        }

        InventoryLoader.Delete(Player.Instance.inventory, this);
        Destroy(this.gameObject);
    }

    protected override void WorldBehaviour(GameObject player, params object[] param)
    {
        if (player == null) return;

    }

    protected override void CombatBehaviour(GameObject player, params object[] param)
    {
        if (player == null) return;

    }
}
