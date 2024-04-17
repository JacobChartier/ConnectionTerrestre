using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_magic_essence");

        Name = "Essence Magique (Ultime)";
        Description = "Essence puissante qui régénère tout ton <color=#00FFFF>MP</color>. Peut être utilisé en ou hors combat. Ne peut pas être acheté, est seulement laissé par des ennemis puissants.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.ESSENCE;

        IsBuyable = false;
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().MagicPoint.Max * 1.0f;

        player.GetComponent<EntityStats>().MagicPoint.Add(value);

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
