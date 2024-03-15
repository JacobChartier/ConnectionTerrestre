using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    DEFAULT
}

public class Enemy : InteractableObjectBase
{
    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private string text;

    public override void Interact()
    {
        SceneManager.LoadScene(2);
    }

    public override void ShowContextLabel()
    {
        //TMP_Text labelText = label.GetComponentInChildren<TMP_Text>();

        //labelText.text = text;
        //label.SetActive(true);
    }

    public override void HideContextLabel()
    {
        //label.SetActive(false);
    }
}
