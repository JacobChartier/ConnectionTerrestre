using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/ultimate_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/ultimate_magic_essence");

        Name = "Essence MAgique (Ultime)";
        Description = "Essence puissante qui r�g�n�re tout ton <color=#00FFFF>MP</color>. Peut �tre utilis� en ou hors combat. Ne peut pas �tre achet�, est seulement laiss� par des ennemis puissants.";

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
