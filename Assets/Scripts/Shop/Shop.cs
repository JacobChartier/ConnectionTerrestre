using Assets.Scripts.Interactables;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : InteractableObjectBase
{
    public Inventory inventory;

    [Header("Shop")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private CinemachineVirtualCamera shopVCAM;

    private void Start()
    {
        LoadShop();
        CameraManager.Instance?.cameras.Add(shopVCAM);
    }


    private void LoadShop()
    {
        inventory.slots = shopMenu.GetComponent<ShopUI>().ShopInventory;
        inventory.id = $"shop-0{Random.Range(1, 4)}";

        InventoryLoader.Load(inventory);
    }

    public override void Interact()
    {
        shopMenu.GetComponent<ShopUI>().Show();

        CameraManager.Instance?.SetCamera(shopVCAM);

        inventoryMenu.gameObject.transform.localPosition = new Vector3(-160, inventoryMenu.transform.localPosition.y, inventoryMenu.transform.localPosition.z);
        inventoryMenu.GetComponent<InventoryUI>().Show();

        InventoryLoader.Load(inventory);
        shopMenu.GetComponent<ShopUI>().SelectItem(shopMenu.GetComponent<ShopUI>().ShopInventory[7]);
        var debugItem = shopMenu.GetComponent<ShopUI>().ShopInventory[7].GetComponentInChildren<DebugItem>(true);
        debugItem.Description += $"INVENTORY_TYPE: Shop\n";
        debugItem.Description += $"INVENTORY_ID: {inventory.id}\n";
        debugItem.Description += $"\nNUMBER_OF_ITEM: {inventory.items.Count - 1}\n";
        debugItem.Price = -1;
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Open Shop");
    }
}
