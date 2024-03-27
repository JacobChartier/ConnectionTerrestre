using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEnable = true;

    public bool isTooltipVisible = true;
    public bool isDescriptionVisible = true;

    public bool isOccupied = false;

    [SerializeField] Color default_color;
    [SerializeField] Color highlight_color;

    public void Start()
    {
        GetComponent<Image>().color = default_color;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (isEnable)
        {
            GameObject droppedItem = eventData.pointerDrag;
            Draggable item = droppedItem.GetComponent<Draggable>();

            if (isOccupied && droppedItem.GetComponent<Item>().Name == GetItem().Name)
            {
                item.parentAfterDrag = transform;
                droppedItem.transform.position = new Vector2(transform.position.x, gameObject.transform.position.y);

            }

            if (!isOccupied)
            {
                item.parentAfterDrag = transform;
                droppedItem.transform.position = new Vector2(transform.position.x, gameObject.transform.position.y);
            }

            isOccupied = true;
            Draggable[] items = GetComponentsInChildren<Draggable>();

            if (items.Count() > 0)
            {
                items[0].count++;
                //items[0].RefreshCount();

                foreach (var i in items)
                {
                    i.enabled = false;
                }

                items[0].enabled = true;
            }
        }
    }

    public Item GetItem()
    {
        return GetComponentInChildren<Item>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<UnityEngine.UI.Image>().color = highlight_color;

        if (isTooltipVisible && isOccupied)
        {
            if (isDescriptionVisible)
            {
                Tooltip.Instance.Show(GenerateTooltipTitleString(gameObject.GetComponentInChildren<Draggable>()?.item), GenerateTooltipString(gameObject.GetComponentInChildren<Draggable>()?.item));
            }
            else
            {
                Tooltip.Instance.Show(GenerateTooltipTitleString(gameObject.GetComponentInChildren<Draggable>()?.item));
            }

            switch (this.gameObject.GetComponentInChildren<Draggable>()?.item.Rarety)
            {
                case Rarety.LEGENDARY:
                    Tooltip.Instance.SetColors(bg: new Color(0.01960784f, 0.01960784f, 0.01960784f), outlines: new Color(1.0f, 0.8431373f, 0.0f));
                    break;

                case Rarety.EPIC:
                    Tooltip.Instance.SetColors(bg: new Color(0.01960784f, 0.01960784f, 0.01960784f), outlines: new Color(0.9725491f, 0.2313726f, 1.0f));
                    break;

                case Rarety.RARE:
                    Tooltip.Instance.SetColors(bg: new Color(0.01960784f, 0.01960784f, 0.01960784f), outlines: new Color(0.2196079f, 0.2980392f, 1.0f));
                    break;

                default:
                    Tooltip.Instance.SetColors(bg: new Color(0.01960784f, 0.01960784f, 0.01960784f), outlines: new Color(1, 1, 1));
                    break;

            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<UnityEngine.UI.Image>().color = default_color;

        if (isTooltipVisible)
        {
            Tooltip.Instance.Hide();
        }
    }

    private string GenerateTooltipString(Item item)
    {
        string tooltip = "";

        tooltip += "<br>";

        tooltip += item.Description;

        if (item.IsUsableOnlyInCombat | item.IsUsableOnlyInOverworld | item.IsBreakable)
            tooltip += "<br>";

        if (item.IsBreakable)
            tooltip += $"<br><color=#505050>Utilisations restantes: {(item.RemainingUses < (item.MaxUses / 3) ? $"<color=#FF0000>{item.RemainingUses}</color>" : $"{item.RemainingUses}")}/{item.MaxUses}</color>";

        // Use Only In

        if (item.IsUsableOnlyInCombat)
            tooltip += "<br><color=#FF0F0F>Peut seulement être utilisé dans le monde de combat.</color>";

        if (item.IsUsableOnlyInOverworld)
            tooltip += "<br><color=#FF0F0F>Peut seulement être utilisé en dehors du monde de combat.</color>";


        return tooltip;
    }

    public string GenerateTooltipTitleString(Item item)
    {
        string tooltip = $"<size=+5><b>{item.Name}";

        switch (item.Rarety)
        {
            case Rarety.LEGENDARY:
                tooltip += $"<br><color=#FFD700>LEGENDARY</color>";
                break;

            case Rarety.EPIC:
                tooltip += $"<br><color=#F83BFF>EPIC</color>";
                break;

            case Rarety.RARE:
                tooltip += $"<br><color=#384CFF>RARE</color>";
                break;

            case Rarety.COMMON:
                tooltip += $"<br><color=#FFFFFF>COMMON</color>";
                break;

            default:
                tooltip += "";
                break;
        }

        return tooltip;
    }
}

[Serializable]
public struct SlotData
{
    public Slot Slot;
    public GameObject Item;
}
