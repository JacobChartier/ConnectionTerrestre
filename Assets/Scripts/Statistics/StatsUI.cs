using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    [SerializeField] private Slider HPBar, MPBar;
    [SerializeField] private TMP_Text HPtext, MPtext;

    private void Update()
    {
        HPBar.value = EntityStats.currentHP;
        HPtext.text = $"{EntityStats.currentHP} HP";

        MPBar.value = EntityStats.currentMP;
        MPtext.text = $"{EntityStats.currentMP} MP";
    }
}
