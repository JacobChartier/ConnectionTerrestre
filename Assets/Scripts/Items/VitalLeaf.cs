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

    public override void Use()
    {
        Debug.LogWarning($"Item use has not been implemented.", this);

        Player.Instance.inventory.Remove(this);
        Destroy(this.gameObject);
    }
}
