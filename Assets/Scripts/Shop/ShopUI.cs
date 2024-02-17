using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Item SelectedItem;

    [SerializeField] private EntityStats playerStats;
    [SerializeField] private TMP_Text _playerCoins;

    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemCategory;
    [SerializeField] private TMP_Text _itemDescription;
    [SerializeField] private TMP_Text _stats;
    [SerializeField] private TMP_Text _itemPrice;

    void Start()
    {
        ShowDescription(SelectedItem);
    }

    private void Update()
    {
        ShowDescription(SelectedItem);
    }

    public void ShowDescription(Item item)
    {
        _playerCoins.text = $"{playerStats.Coins.Current.ToString()}";

        this._itemName.text = item.item_name;
        this._itemCategory.text = item.type.ToString();
        this._itemDescription.text = item.description;
        this._itemPrice.text = $"<b>{item.price} <color=#FFD700>Coins</color></b>";

        this._stats.text = "";
        for (int i = 0; i < item.stats.Count(); i++)
        {
            this._stats.text += $"{item.stats.ElementAt(i)}\n";
        }
    }
}
