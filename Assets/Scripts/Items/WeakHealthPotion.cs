using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class WeakHealthPotion : Item
{
    GameObject prefab;

    protected override void Load()
    {
        prefab = Resources.Load<GameObject>("Prefabs/textprefab");

        Icon = Resources.Load<Sprite>("Sprites/Items/weak_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/weak_health_potion");

        Name = "Health Potion (Weak)";
        Description = "Potion faible qui régénère 15% de ton <color=#FF2E2E>HP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.POTION;
    }

    public override void Use()
    {
        var player = GameObject.Find("Player");
        var hpAdded = player.GetComponent<EntityStats>().Health.Max * 0.15f;
        player.GetComponent<Health>().AddHealthPoint(hpAdded);

        var obj = Instantiate(prefab, new Vector2(GameObject.Find("Player Health").transform.position.x, GameObject.Find("Player Health").transform.position.y), Quaternion.identity, GameObject.Find("Menu").transform);
        obj.GetComponent<TextAnimation>().StartAnimation(Random.Range(0.5f, 1.5f), $"+{hpAdded} HP");

        Destroy(this.gameObject);
    }
}
