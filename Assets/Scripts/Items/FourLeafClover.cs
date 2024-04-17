using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeafClover : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/four_leaf_clover");
        Model = Resources.Load<Mesh>("Meshes/Items/four_leaf_clover");

        Name = "Tr�fle � Quatre Feuilles";
        Description = "Augmente de 50% la <color=#FFD700>monnaie</color> et l'<color=#91FF00>exp�rience</color> que le joueur re�oit lors de la d�faite d�un ennemi. Cet item peut seulement �tre utilis� hors combat. L'effet dure 5 utilisations.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.LEAF;

        IsUsableOnlyInOverworld = true;

        IsBreakable = true;
        MaxUses = 5;

        Price = GeneratePrice(18, 27);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = 0.50f;

        RemainingUses--;
        InventoryLoader.Update(Player.Instance.inventory, this);

        switch (scene)
        {
            case Scenes.WORLD:
                WorldBehaviour(player, value);
                break;

            case Scenes.COMBAT:
                CombatBehaviour(player, value);
                break;
        }

        if (RemainingUses < 1)
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
