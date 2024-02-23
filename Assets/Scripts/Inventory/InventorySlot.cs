using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDraggable = true;
    public bool isEnable = true;
    
    public bool isTooltipVisible = true;
    public bool isDescriptionVisible = true;

    public bool isOccupied = false;

    public void OnDrop(PointerEventData eventData)
    {
        isOccupied = true;

        if (isEnable)
        {
            if (transform.childCount == 0)
            {
                GameObject droppedItem = eventData.pointerDrag;
                DraggableItem item = droppedItem.GetComponent<DraggableItem>();

                item.parentAfterDrag = transform;
            }
        }
    }

    public Item GetItemInSlot()
    {
        return GetComponentInChildren<DraggableItem>().item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isTooltipVisible && isOccupied)
        {
            if (isDescriptionVisible)
            {
                Tooltip.Instance.Show(this.gameObject.GetComponentInChildren<DraggableItem>().item.tooltipName, this.gameObject.GetComponentInChildren<DraggableItem>().item.tooltipDescription);
            }
            else
            {
                Tooltip.Instance.Show(this.gameObject.GetComponentInChildren<DraggableItem>().item.tooltipName);
            }

            switch (this.gameObject.GetComponentInChildren<DraggableItem>().item.rarety)
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
        if (isTooltipVisible)
        {
            Debug.Log($"exited {this.gameObject.name}");

            Tooltip.Instance.Hide();
        }
    }
}
