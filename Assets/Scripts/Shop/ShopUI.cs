using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MenuHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private EntityStats playerStats;

    [SerializeField] private new TMP_Text name;
    [SerializeField] private TMP_Text category;
    [SerializeField] private TMP_Text rarety;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text price;
    [SerializeField] private Button purchaseButton;

    [SerializeField] public Item SelectedItem;
    [SerializeField] private InventorySlot[] ShopInventory;

    void Start()
    {
        ResetDescription();
        this.purchaseButton.onClick.AddListener(delegate { AddToInventory(); });
    }

    private void OnEnable()
    {
        CameraManager.Instance.EnableFreeCameraMovement(false);
    }

    private void ResetOutlines()
    {
        foreach (var item in ShopInventory)
        {
            item.GetComponent<Outline>().enabled = false;
        }
    }

    public void SelectItem(InventorySlot slot)
    {
        SelectedItem = slot.GetComponentInChildren<DraggableItem>()?.item;

        ResetOutlines();
        slot.GetComponent<Outline>().enabled = !slot.GetComponent<Outline>().enabled;

        ShowDescription(SelectedItem);
    }

    private void AddToInventory()
    {
        if (SelectedItem.Purchase(playerStats))
        {
            player.GetComponentInChildren<Inventory>()?.Add(SelectedItem);

        }

        ShowPrice(SelectedItem);
    }

    public void ShowDescription(Item item)
    {
        this.name.text = item.Name;

        switch (item.Rarety)
        {
            case Rarety.COMMON:
                this.rarety.text = "<color=#FFFFFF>Common</color>";
                break;

            case Rarety.RARE:
                this.rarety.text = "<color=#384CFF>Rare</color>";
                break;

            case Rarety.EPIC:
                this.rarety.text = "<color=#F83BFF>Epic</color>";
                break;

            case Rarety.LEGENDARY:
                this.rarety.text = "<color=#FFD700>Legendary</color>";
                break;

        }

        switch (item.Category)
        {

            case Category.POTION:
                this.category.text = "<color=#F83BFF>Potion</color>";
                break;

            case Category.ESSENCE:
                this.category.text = "<color=#4ADE2C>Essence</color>";
                break;

            case Category.SHIELD:
                this.category.text = "<color=#853815>Shield</color>";
                break;

            case Category.LEAF:
                this.category.text = "<color=#1C9C02>Clover</color>";
                break;

            case Category.DEBUG:
                this.category.text = "<color=#FFFFFF>Other</color>";
                break;

            default:
                this.category.text = $"<color=#FFFFFF>{item.Category}</color>";
                break;
        }

        this.description.text = item.Description;

        ShowPrice(item);

    }

    private void ShowPrice(Item item)
    {
        switch (item.price)
        {
            case 0:
                this.price.text = $"<b><color=#17FF3E>FREE</color></b>";
                break;
            default:
                this.price.text = (item.price > playerStats.Coins.Current) ? $"<b><color=#FF0000>{item.price}</color></b>" : $"<b><color=#FFFFFF>{item.price}</color></b>";
                break;
        }
    }

    private void ResetDescription()
    {
        this.name.text = "";
        this.category.text = "";
        this.rarety.text = "";
        this.description.text = "";
        this.price.text = "<b>0</b>";
    }

    public void Show()
    {
        InputManager.controls?.Player.Disable();
        InputManager.controls?.Menus.Enable();

        CameraManager.Instance?.FreezeCamera(true);

        this.transform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        InputManager.controls?.Player.Enable();
        InputManager.controls?.Menus.Disable();

        CameraManager.Instance?.FreezeCamera(false);
        this.transform.gameObject.SetActive(false);

        Tooltip.Instance?.Hide();

        GetMenu<InventoryUI>().Hide();
    }
}
