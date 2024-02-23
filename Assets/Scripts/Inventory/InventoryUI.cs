using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private EntityStats player;

    [SerializeField] private TMP_Text coins;

    private void Awake()
    {
        Instance = this;

        Show();
    }

    private void Show()
    {
        this.coins.text = player.Coins.Current.ToString();
    }

    public void Refresh()
    {
        this.coins.text = player.Coins.Current.ToString();
    }
}
