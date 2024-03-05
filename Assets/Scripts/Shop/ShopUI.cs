using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IMenuHandler
{
    public static ShopUI Instance;

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
        if (Instance != null)
            Instance = this;

        ResetDescription();
        this.purchaseButton.onClick.AddListener(delegate { AddToInventory(); });
    }

    private void OnEnable()
    {
        CameraController.Instance.EnableFreeCameraMovement(false);
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
        SelectedItem = slot.GetComponentInChildren<DraggableItem>().item;

        ResetOutlines();
        slot.GetComponent<Outline>().enabled = !slot.GetComponent<Outline>().enabled;

        ShowDescription(SelectedItem);
    }

    private void AddToInventory()
    {
        if (SelectedItem.Purchase(playerStats))
        {
            player.GetComponentInChildren<Inventory>().Add(SelectedItem);
            InventoryUI.Instance.Refresh();
        }

        ShowPrice(SelectedItem);
    }

    public void ShowDescription(Item item)
    {
        this.name.text = item.name;

        switch (item.rarety)
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

        switch (item.category)
        {
            case Type.WEAK_HEALTH_POTION:
            case Type.NORMAL_HEALTH_POTION:
            case Type.STRONG_HEALTH_POTION:
            case Type.ULTIMATE_HEALTH_POTION:
            case Type.NORMAL_EXPERIENCE_POTION:
                this.category.text = "<color=#F83BFF>Potion</color>";
                break;

            case Type.WEAK_MAGIC_ESSENCE:
            case Type.NORMAL_MAGIC_ESSENCE:
            case Type.STRONG_MAGIC_ESSENCE:
            case Type.ULTIMATE_MAGIC_ESSENCE:
            case Type.NORMAL_WARRIOR_ESSENCE:
            case Type.NORMAL_MAGICIAN_ESSENCE:
                this.category.text = "<color=#4ADE2C>Essence</color>";
                break;

            case Type.SHIELD:
                this.category.text = "<color=#853815>Shield</color>";
                break;

            case Type.FOUR_LEAF_CLOVER:
                this.category.text = "<color=#1C9C02>Clover</color>";
                break;

            case Type.VITAL_LEAF:
                this.category.text = "<color=#1C9C02>Leaf</color>";
                break;

            case Type.OTHER:
                this.category.text = "<color=#FFFFFF>Other</color>";
                break;

            default:
                this.category.text = $"<color=#FFFFFF>{item.category}</color>";
                break;
        }

        this.description.text = item.description;

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

        CameraController.Instance?.FreezeCamera(true);

        this.transform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        InputManager.controls?.Player.Enable();
        InputManager.controls?.Menus.Disable();

        CameraController.Instance?.FreezeCamera(false);
        this.transform.gameObject.SetActive(false);

        Tooltip.Instance?.Hide();

        InventoryUI.Instance?.Hide();
    }
}
