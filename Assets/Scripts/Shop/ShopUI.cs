using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopUI : MenuHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private EntityStats playerStats;

    [SerializeField] private new TMP_Text name;
    [SerializeField] private TMP_Text category;
    [SerializeField] private TMP_Text rarety;

    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject descSV;

    [SerializeField] private TMP_Text price;
    [SerializeField] private UnityEngine.UI.Button purchaseButton;

    [SerializeField] public Item SelectedItem;
    [SerializeField] public Slot[] ShopInventory;

    void Start()
    {
        ResetDescription();
        this.purchaseButton.onClick.AddListener(delegate { AddToInventory(); });

        Canvas.ForceUpdateCanvases();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        ResetDescription();
        ResetOutlines();

        foreach (var slot in ShopInventory)
        {
            Destroy(slot.GetComponentInChildren<Item>()?.gameObject);
        }
    }

    private void ResetOutlines()
    {
        foreach (var slot in ShopInventory)
        {
            slot.GetComponent<Outline>().enabled = false;
        }
    }

    public void SelectItem(Slot slot)
    {
        SelectedItem = slot.GetComponentInChildren<Item>();

        if (SelectedItem is null)
        {
            ResetOutlines();
            ResetDescription();

            descSV.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            return;
        }

        ResetOutlines();
        slot.GetComponent<Outline>().enabled = true;

        ShowDescription(SelectedItem);
        DescriptionScrollBar();
    }

    private void DescriptionScrollBar()
    {
        description.ForceMeshUpdate();

        var textHeight = description.GetRenderedValues().y;
        descSV.GetComponent<RectTransform>().sizeDelta = new Vector2(0, textHeight);
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
                this.rarety.text = "<color=#FFFFFF>COMMUN</color>";
                break;

            case Rarety.RARE:
                this.rarety.text = "<color=#384CFF>RARE</color>";
                break;

            case Rarety.EPIC:
                this.rarety.text = "<color=#F83BFF>ÉPIQUE</color>";
                break;

            case Rarety.LEGENDARY:
                this.rarety.text = "<color=#FFD700>LÉGENDAIRE</color>";
                break;

            default:
                this.rarety.text = $"<color=#FFFFFF>???</color>";
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
                this.category.text = "<color=#853815>Bouclier</color>";
                break;

            case Category.LEAF:
                this.category.text = "<color=#1C9C02>Feuille</color>";
                break;

            default:
                this.category.text = $"<color=#FFFFFF>???</color>";
                break;
        }

        this.description.text = item.Description;

        ShowPrice(item);

    }

    private void ShowPrice(Item item)
    {
        switch (item.Price)
        {
            case 0:
                this.price.text = $"<b><color=#17FF3E>FREE</color></b>";
                break;
            default:
                this.price.text = (item.Price > playerStats.Coins) ? $"<b><color=#FF0000>{item.Price}</color></b>" : $"<b><color=#FFFFFF>{item.Price}</color></b>";
                break;
        }
    }

    private void ResetDescription()
    {
        this.name.text = "";
        this.category.text = "";
        this.rarety.text = "";
        this.description.text = "";
        this.price.text = "";
    }

    public override void Show()
    {
        base.Show();

        GetMenu<InventoryUI>().Show();
    }

    public override void Hide()
    {
        base.Hide();

        Tooltip.Instance?.Hide();

        GetMenu<InventoryUI>().Hide();
    }
}
