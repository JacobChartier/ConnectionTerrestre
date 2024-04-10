using Assets.Scripts.Interactables;
using Cinemachine;
using System;
using TMPro;
using UnityEngine;

public abstract class InteractableObjectBase : MonoBehaviour, IInteractable
{
    private void Start()
    {
    }
    
    public abstract void Interact();

    public abstract void ShowContextLabel();

    public void HideContextLabel()
    {
        ContextLabelUI.Instance.HideContextLabel();
    }

    public virtual void EnableFreeCameraMovement(bool isEnable)
    {
        if (isEnable)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public virtual void SwitchVirtualCameraPriority(CinemachineVirtualCamera playerVCam, CinemachineVirtualCamera OtherVCam, bool setVCamToPlayer = false)
    {
        if (setVCamToPlayer)
        {
            OtherVCam.Priority = 0;
            playerVCam.Priority = 1;
        }
        else
        {
            OtherVCam.Priority = 1;
            playerVCam.Priority = 0;
        }
    }
}