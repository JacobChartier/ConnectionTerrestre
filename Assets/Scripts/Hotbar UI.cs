using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Slot[] slots = new Slot[10];
    [SerializeField] private Slot selectedSlot = null;

    private int index = 0;

    private void Start()
    {
        selectedSlot = slots[0];
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
            slot.GetComponent<Image>().color = Color.black;
        }

        selectedSlot.GetComponent<Image>().color = Color.white;
    }

    public void SelectSlot(int mouseInput)
    {
        if (mouseInput >= 1)
            NextSlot();

        if (mouseInput <= 1)
            PreviousSlot();
    }

    private void NextSlot()
    {
        if (index == 9) 
            index = 0;
        
        index++;
    }

    private void PreviousSlot()
    {
        if (index == 0)
            index = slots.Length;

        index--;
    }
}
