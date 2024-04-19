using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HeadUpDisplay : MenuHandler
{
    public static HeadUpDisplay Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GameObject.Find("Player").GetComponent<Health>().onGainHealth.AddListener(UpdateUI);
        GameObject.Find("Player").GetComponent<Health>().onLostHealth.AddListener(UpdateUI);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        var healthText = GameObject.Find("Player Health");
        healthText.GetComponent<TMP_Text>().text = $"{GameObject.Find("Player").GetComponent<EntityStats>().Health.Current} / {GameObject.Find("Player").GetComponent<EntityStats>().Health.Max}";

        var healthbar = GameObject.Find("Health Bar");
        healthbar.GetComponent<Slider>().maxValue = GameObject.Find("Player").GetComponent<EntityStats>().Health.Max;
        healthbar.GetComponent<Slider>().value = GameObject.Find("Player").GetComponent<EntityStats>().Health.Current;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject.Find("Player").GetComponent<Health>().RemoveHealthPoint(10);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject.Find("Player").GetComponent<Health>().AddHealthPoint(10);
        }
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
