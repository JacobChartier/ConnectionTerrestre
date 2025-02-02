using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeakMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/weak_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/weak_magic_essence");

        Name = "Essence Magique (Faible)";
        Description = "Essence faible qui r�g�n�re 15% de ton <color=#00FFFF>MP</color> maximal. Peut �tre utilis� en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.ESSENCE;

        Price = GeneratePrice(2, 5);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().MagicPoint.Max * 0.15f;

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
