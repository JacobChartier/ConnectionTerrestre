using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Shield : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/shield");
        Model = Resources.Load<Mesh>("Meshes/Items/shield");

        Name = "Bouclier";
        Description = "Si utilisé en combat, ta <color=#213BFF>défense</color> est augmentée pendant au moins 3 tours. Pour chaque tour après le 3e en effet, le bouclier a 50% de chance de briser. <br><br>Si utilisé hors combat, pour un certain temps, si tu entre en combat, l’effet du bouclier sera automatiquement appliqué sur toi.";

        Rarety = Rarety.EPIC;
        Category = Category.SHIELD;
        
        Price = GeneratePrice(10, 17);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().Defense.Max * 0.15f;

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

        if (Random.Range(0, 2) == 1)
        {
            InventoryLoader.Delete(Player.Instance.inventory, this);
            Destroy(this.gameObject);
        }
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
