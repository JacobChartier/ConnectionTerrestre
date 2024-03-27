using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleDataTransfer : MonoBehaviour
{
    [SerializeField] public EntityStats player_stats;
    [SerializeField] public EntityStats enemy_stats;
    [SerializeField] public Inventory inventory;

    public static BattleDataTransfer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        instance.player_stats = player_stats;
        instance.enemy_stats = enemy_stats;
        instance.inventory = inventory;
    }

    //public void TransferData()
    //{
    //    player_stats = player;
    //    enemy_stats = enemy;
    //    this.inventory = inventory;
    //}
    //public void ReturnDataToWorld(Scene scene, LoadSceneMode mode) // inséré dans delegate, ne pas supprimer paramètres
    //{
    //    player_stats = GetPlayer();
    //    enemy_stats = GetEnemy();
    //    Debug.Log("fuck 1");
    //    inventory.items = GetInventory();
    //    Debug.Log("fuck 2");
    //}

    //public EntityStats GetPlayer()
    //{
    //    return player_stats;
    //}

    //public EntityStats GetEnemy()
    //{
    //    return enemy_stats;
    //}

    //public Item[] GetInventory()
    //{
    //    return inventory.items;
    //}
}
