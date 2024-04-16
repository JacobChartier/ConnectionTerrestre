using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WeakHealthPotion : Item
{
    GameObject prefab;

    protected override void Load()
    {
        prefab = Resources.Load<GameObject>("Prefabs/textprefab");

        Icon = Resources.Load<Sprite>("Sprites/Items/weak_health_potion");
        Model = Resources.Load<Mesh>("Meshes/Items/weak_health_potion");

        Name = "Potion de Vie (Faible)";
        Description = "Potion faible qui régénère 15% de ton <color=#FF2E2E>HP</color> maximal. Peut être utilisé en ou hors combat.";

        Rarety = Rarety.COMMON;
        Category = Category.POTION;

        Price = GeneratePrice(2, 5);
    }

    public override void Use(Scenes scene)
    {
        var player = GameObject.Find("Player");
        var value = player.GetComponent<EntityStats>().Health.Max * 0.15f;

        player.GetComponent<Health>().AddHealthPoint(value);

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

        var obj = Instantiate(prefab, new Vector2(GameObject.Find("Player Health").transform.position.x, GameObject.Find("Player Health").transform.position.y), Quaternion.identity, GameObject.Find("Menu").transform);
        obj.GetComponent<TextAnimation>().StartAnimation(Random.Range(0.5f, 1.5f), $"+{value} HP");
    }

    private void UseInCombat(GameObject player, float value)
    {
        if (player == null) return;

    }
}
