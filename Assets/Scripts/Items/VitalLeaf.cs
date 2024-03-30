using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalLeaf : Item
{
    protected override void Load()
    {
        Icon = Resources.Load<Sprite>("Sprites/Items/vital_leaf");
        Model = Resources.Load<Mesh>("Meshes/Items/vital_leaf");

        Name = "Vital Leaf";
        Description = "Si cette feuille magique se trouve dans ton inventaire quand tu meurs au combat, tu reviens en vie avec ¼ de ton HP maximal. Ne peut pas être utilisé hors combat.";

        Rarety = Rarety.LEGENDARY;
        Category = Category.LEAF;

        IsUsableOnlyInCombat = true;
    }

    public override void Use()
    {
        Debug.LogWarning($"Item use has not been implemented.", this);
        Destroy(this.gameObject);
    }
}
