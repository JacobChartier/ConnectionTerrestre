using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : InteractableObjectBase
{
    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private string text;

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
        inventory.Add(ItemManager.Instance.items.ElementAt(0));
        inventory.Add(ItemManager.Instance.items.ElementAt(1));

        shopMenu.GetComponent<ShopUI>().Show();

        CameraManager.Instance?.EnableFreeCameraMovement(false);
        CameraManager.Instance?.SetCamera(shopVCAM);

        CameraManager.Instance?.FreezeCamera(true);

        inventoryMenu.gameObject.transform.position = new Vector3((Camera.main.scaledPixelWidth / 2) - 155, inventoryMenu.transform.position.y, inventoryMenu.transform.position.z);
        inventoryMenu.GetComponent<InventoryUI>().Show();
    }

    public override void ShowContextLabel()
    {
        TMP_Text labelText = label.GetComponentInChildren<TMP_Text>();

        labelText.text = text;
        label.SetActive(true);
    }

    public override void HideContextLabel()
    {
        label.SetActive(false);
    }
}
