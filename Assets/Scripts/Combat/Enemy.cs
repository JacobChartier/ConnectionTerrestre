using Assets.Scripts.Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Enemy : InteractableObjectBase
{
    public override void Interact()
    {
        SceneManager.LoadScene(2);
    }

    public override void ShowContextLabel()
    {
        ContextLabelUI.Instance.ShowContextLabel("E", "Enter Combat");
    }
}
