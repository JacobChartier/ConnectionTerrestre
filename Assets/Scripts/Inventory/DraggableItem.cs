using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TMP_Text countText;
    public Slider durability;

    public Item item;
    [HideInInspector] public int count;

    [HideInInspector] public Transform parentAfterDrag;
    InventorySlot originalSlot;

    private void Start()
    {
        InitialiseItem(item);
        RefreshCount();
    }

    public void InitialiseItem(Item item)
    {
        parentAfterDrag = transform.parent;
        originalSlot = transform.parent.GetComponent<InventorySlot>();

        transform.parent.GetComponent<InventorySlot>().isOccupied = true;

        this.item = item;
        image.sprite = item.Icon;

        if (item.isBreakable)
        {
            durability.gameObject.SetActive(true);

            durability.gameObject.GetComponent<Slider>().maxValue = item.MaxDurability;
            durability.gameObject.GetComponent<Slider>().value = item.Durability;
        }
        else
        {
            durability.gameObject.SetActive(false);
        }
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();

        bool isCounterActive = count > 1;
        countText.gameObject.transform.parent.gameObject.SetActive(isCounterActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        int currentStack = item.StackSize;

        if (InputManager.mouseButtonInput == MouseButton.RIGHT) // Split stack in half
        {
            currentStack /= 2;
        }

        originalSlot.isOccupied = false;

        if (originalSlot.isEnable)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            image.raycastTarget = false;
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
            transform.SetParent(parentAfterDrag);

            image.raycastTarget = true;
        }
    }
}
