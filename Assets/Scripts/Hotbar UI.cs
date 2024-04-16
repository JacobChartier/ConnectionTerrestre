using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Slot[] slots = new Slot[10];
    [SerializeField] private Slot selectedSlot = null;
    //[SerializeField] private TMP_Text itemNameLabel;

    private Color initialColor = new Color(0.01960784f, 0.01960784f, 0.01960784f, 0.8627451f);
    private Color selectionColor = new Color(0.135f, 0.135f, 0.135f, 0.8627451f);

    private int index = 0;

    private void Start()
    {
        selectedSlot = slots[0];

        InputManager.controls.Player.UseSelectedItem.performed += UseSelectedItem;
    }

    private void Update()
    {
        SelectSlot((int)InputManager.scrollWheelInput);
        UpdateUI();
    }

    private void UpdateUI()
    {
        selectedSlot = slots[index];

        foreach (Slot slot in slots)
        {
            slot.GetComponent<Image>().color = initialColor;
        }

        selectedSlot.GetComponent<Image>().color = selectionColor;
    }

    public void SelectSlot(int mouseInput)
    {
        if (mouseInput <= -1)
        {
            NextSlot();
        }

        if (mouseInput >= 1)
        {
            PreviousSlot();
        }

        //if(selectedSlot.GetComponentInChildren<Item>() != null)
        //{
        //    itemNameLabel.text = "";

        //    itemNameLabel = selectedSlot.gameObject.GetComponentInChildren<TMP_Text>();
        //    itemNameLabel.text = selectedSlot.GetItem().Name;
        //}
        //else
        //{
        //    itemNameLabel.text = "";
        //}
    }

    private void NextSlot()
    {
        index++;

        if (index > 9)
            index = 0;

        return;
    }

    private void PreviousSlot()
    {
        if (index == 0)
            index = slots.Length;

        index--;

        return;
    }


    private void UseSelectedItem(InputAction.CallbackContext ctx)
    {
        if (selectedSlot.GetComponentInChildren<Item>() == null) return;

        selectedSlot.GetItem().Use(Scenes.WORLD);
        HeadUpDisplay.Instance.UpdateUI();
    }
}
