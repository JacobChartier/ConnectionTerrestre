using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Item))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    private GameObject renderedItem;
    private Slot currentSlot;

    public TMP_Text countText;
    public Slider durability;

    public int count;

    public Transform parentAfterDrag;
    public Slot originalSlot;

    public void InitialiseItem()
    {
        if (transform.parent.GetComponent<Slot>() == null) return;

        renderedItem = gameObject.transform.GetChild(0).gameObject;
        currentSlot = transform.GetComponentInParent<Slot>(true);
        originalSlot = transform.GetComponentInParent<Slot>(true);

        transform.parent.GetComponentInParent<Slot>().isOccupied = true;

        item.transform.localPosition = Vector2.zero;

        renderedItem.GetComponent<Image>().sprite = item.Icon;
        renderedItem.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);

        if (item.IsBreakable)
            this.GetComponentInChildren<Slider>().gameObject.SetActive(true);
        else
            this.GetComponentInChildren<Slider>().gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalSlot.isOccupied = false;

        if (originalSlot.isEnable)
        {
            parentAfterDrag = gameObject.transform.parent;
            transform.SetParent(transform.root.root);
            transform.SetAsLastSibling();

            GetComponentInChildren<Image>().raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Tooltip.Instance?.Hide();

        if (originalSlot.isEnable)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (originalSlot.isEnable)
        {
            gameObject.transform.SetParent(parentAfterDrag);
            GetComponentInChildren<Image>().raycastTarget = true;
            transform.position = this.gameObject.transform.position;
        }

        transform.position = parentAfterDrag.transform.position;

        countText.text = count.ToString();
    }
}
