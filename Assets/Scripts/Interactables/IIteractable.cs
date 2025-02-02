using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();

    public void ShowContextLabel();

    public void HideContextLabel();
}
