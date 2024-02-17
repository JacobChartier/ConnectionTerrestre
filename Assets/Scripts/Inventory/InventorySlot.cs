using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isDraggable = true;

    public void OnDrop(PointerEventData eventData)
    {
        if (isDraggable && transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            DraggableItem item = droppedItem.GetComponent<DraggableItem>();

            item.parentAfterDrag = transform;
        }
    }
}
