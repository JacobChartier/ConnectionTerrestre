using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Item SelectedItem;

    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCategory;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text stats;
    [SerializeField] private TMP_Text itemPrice;

    void Start()
    {
        ShowDescription(SelectedItem);
    }

    public void ShowDescription(Item item)
    {
        this.itemName.text = item.name;
        this.itemCategory.text = item.category.ToString();
        this.itemDescription.text = item.description;
        this.itemPrice.text = $"<b>{item.price} <color=#FFD700>Coins</color></b>";

        this.stats.text = "";
        for (int i = 0; i < item.stats.Count(); i++)
        {
            this.stats.text += $"{item.stats.ElementAt(i)}\n";
        }
    }
}
