using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : InteractableObjectBase
{
    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private Sprite icon;
    [SerializeField] private string text;

    [Header("Shop")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private CinemachineVirtualCamera playerVCAM;
    [SerializeField] private CinemachineVirtualCamera shopVCAM;

    public override void Interact()
    {
        EnableFreeCameraMovement(false);
    }

    public override void EnableFreeCameraMovement(bool isEnable)
    {
        base.EnableFreeCameraMovement(isEnable);

        if (isEnable)
        {
            playerVCAM.Priority = 0;
            shopVCAM.Priority = 1;
        }
        else
        {
            playerVCAM.Priority = 1;
            shopVCAM.Priority = 0;
        }
    }

    public override void ShowContextLabel()
    {
        TMP_Text labelText = label.GetComponentInChildren<TMP_Text>();

        labelText.text = text;
        label.SetActive(true);
    }

    public override void HideContextLabel()
    {
        label.SetActive(false);
    }
}
