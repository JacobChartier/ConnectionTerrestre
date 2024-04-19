using UnityEngine;

public class MenuHandler : MonoBehaviour, IMenuHandler
{
    public static T GetMenu<T>() where T : MenuHandler
    { return FindObjectOfType<T>(true); }

    public virtual void Show()
    {
        gameObject.SetActive(true);

        CameraManager.EnableInteractiveMode(true);

        InputManager.EnableMenuInputs(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);

        CameraManager.EnableInteractiveMode(false);

        InputManager.EnableMenuInputs(false);
    }
}

