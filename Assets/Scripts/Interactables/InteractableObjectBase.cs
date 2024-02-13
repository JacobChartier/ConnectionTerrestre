using System;
using UnityEngine;

public abstract class InteractableObjectBase : MonoBehaviour, IInteractable
{
    public abstract void Interact();

    public abstract void ShowContextLabel();
    public abstract void HideContextLabel();

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
}