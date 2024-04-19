using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Drawing;

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

        durability = gameObject.GetComponentInChildren<Image>(true).gameObject.GetComponentInChildren<Slider>(true);
        durability.maxValue = item.MaxUses;
    }

    [System.Obsolete]
    private void Update()
    {
        if (item.IsBreakable)
        {
            durability.gameObject.SetActive(true);
            durability.value = item.RemainingUses;

            var fillArea = durability.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();

            if (durability.value < (0.25f * durability.maxValue))
            {
                fillArea.color = new UnityEngine.Color(1.0f, 0.25f, 0.25f, 1.0f);
            }
            else if (durability.value < (0.5f * durability.maxValue))
            {
                fillArea.color = new UnityEngine.Color(0.95f, 1.0f, 0.25f, 1.0f);
            }
            else if (durability.value <= (1.0f * durability.maxValue))
            {
                fillArea.color = new UnityEngine.Color(0.5f, 1.0f, 0.25f, 1.0f);
            }
        }
        else
        {
            durability.gameObject.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift)) return;

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
        if (Input.GetKey(KeyCode.LeftShift)) return;

        Tooltip.Instance?.Hide();

        if (originalSlot.isEnable)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift)) return;

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
