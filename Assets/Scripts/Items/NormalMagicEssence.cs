using UnityEngine;

public class NormalMagicEssence : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/normal_magic_essence");
        Model = Resources.Load<Mesh>("Meshes/Items/normal_magic_essence");

        Name = "Essence Magique";
        Description = "Essence qui r�g�n�re 30% de ton <color=#00FFFF>MP</color> maximal. Peut �tre utilis� en ou hors combat.";

        Rarety = Rarety.RARE;
        Category = Category.ESSENCE;

        Price = GeneratePrice(4, 9);
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");
        player.GetComponent<EntityStats>().MagicPoint.Add(player.GetComponent<EntityStats>().MagicPoint.Max * 0.30f);

        Destroy(this.gameObject);
    }
}
