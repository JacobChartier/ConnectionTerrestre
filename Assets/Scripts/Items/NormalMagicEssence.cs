using UnityEngine;

public class NormalMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_magic_essence");

        Name = "Essence Magique";
        Description = "Essence qui régénère 30% de ton <color=#00FFFF>MP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.RARE;
        Category = Category.ESSENCE;

        Price = GeneratePrice(4, 9);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().MagicPoint.Max * 0.30f;

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
