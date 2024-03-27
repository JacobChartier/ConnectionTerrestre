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
    [SerializeField] private CapsuleCollider collision;

    [Header("Info pour combat")]
    [SerializeField] private EntityStats joueur;
    [SerializeField] private Inventory inventaire;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Player")
            return;

        Interact();
    }

    public override void Interact()
    {
        GameManager.Instance.playerES.Health = GameObject.Find("Player").GetComponent<EntityStats>().Health;
        GameManager.Instance.enemyES = GetComponent<EntityStats>();

        if (BattleInfo.player == null)
        {
            Debug.Log("player null");
        }

        BattleInfo.enemy = GetComponent<EntityStats>();
        BattleInfo.inventory = inventaire;
        SceneManager.LoadScene(2);
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Enter Combat");
    }
}
