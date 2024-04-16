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
        CameraManager.Instance?.cameras.Add(shopVCAM);

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            //var generatedItem = Inventory.GenerateRandomItem();

            //generatedItem.price = generatedItem.GeneratePrice(5, 15);
            //inventory.Add(generatedItem);
        }
    }

    public override void Interact()
    {
        shopMenu.GetComponent<ShopUI>().Show();

        CameraManager.Instance?.EnableFreeCameraMovement(false);
        CameraManager.Instance?.SetCamera(shopVCAM);

        CameraManager.Instance?.FreezeCamera(true);

        inventoryMenu.gameObject.transform.localPosition = new Vector3(-160, inventoryMenu.transform.localPosition.y, inventoryMenu.transform.localPosition.z);
        inventoryMenu.GetComponent<InventoryUI>().Show();

        InventoryLoader.Load(inventory);
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Open Shop");
    }

    //public override void HideContextLabel()
    //{
    //    label.SetActive(false);
    //}
}
