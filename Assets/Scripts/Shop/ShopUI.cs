using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private EntityStats playerStats;
    [SerializeField] private TMP_Text playerCoins;

    [SerializeField] private TMP_Text name;
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
        InventoryManager.instance.Add(SelectedItem);
    }

    public void ShowDescription(Item item)
    {
        playerCoins.text = $"{playerStats.Coins.Current}";

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
            case ItemCategory.POTION:
                this.category.text = "<color=#F83BFF>Potion</color>";
                break;

            case ItemCategory.ESSENCE:
                this.category.text = "<color=#4ADE2C>Essence</color>";
                break;

            case ItemCategory.SHIELD:
                this.category.text = "<color=#853815>Shield</color>";
                break;

            case ItemCategory.OTHER:
                this.category.text = "<color=#FFFFFF>Other</color>";
                break;


        }


        this.description.text = item.description;
        this.price.text = (item.price > playerStats.Coins.Current) ? $"<b><color=#FF0000>{item.price}</color></b>" : $"<b><color=#FFFFFF>{item.price}</color></b>";
    }

    private void ResetDescription()
    {
        this.name.text = "";
        this.category.text = "";
        this.rarety.text = "";
        this.description.text = "";
        this.price.text = "<b>0</b>";
    }
}
