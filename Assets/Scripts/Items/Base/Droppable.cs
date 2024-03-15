using Assets.Scripts.Interactables;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Item))]
public class Droppable : InteractableObjectBase
{
    public Item item = null;
    public Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponentInChildren<Inventory>();
    }

    public override void Interact()
    {
        playerInventory.Add(item);
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Collect Item");
    }
}