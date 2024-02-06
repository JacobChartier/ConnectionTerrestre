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
    [SerializeField] private EntityStats player_stats;

    private void Update()
    {
        HPBar.value = player_stats.currentHP;
        HPtext.text = $"{player_stats.currentHP} HP";

        MPBar.value = player_stats.currentMP;
        MPtext.text = $"{player_stats.currentMP} MP";
    }
}
