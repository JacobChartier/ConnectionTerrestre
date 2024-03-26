using Assets.Scripts.Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    DEFAULT
}

public class Enemy : InteractableObjectBase
{
    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private string text;

    [Header("Info pour combat")]
    [SerializeField] private EntityStats joueur;
    [SerializeField] private Inventory inventaire;


    public override void Interact()
    {
        //BattleInfo.player = joueur;
        //BattleInfo.enemy = GetComponent<EntityStats>();
        //BattleInfo.inventory = inventaire;
        SceneManager.LoadScene(2);
    }

    private void FinDeCombat(Scene arg0, Scene arg1)
    {
        Debug.Log(1);
        joueur = BattleInfo.player;
        inventaire = BattleInfo.inventory;
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Enter Combat");
    }
}
