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
        HPBar.maxValue = player_stats.Health.Max;
        HPBar.minValue = player_stats.Health.Min;

        HPBar.value = player_stats.Health.Current;
        HPtext.text = $"{player_stats.Health.Current} HP";


        MPBar.maxValue = player_stats.MagicPoint.Max;
        MPBar.minValue = player_stats.MagicPoint.Min;

        MPBar.value = player_stats.MagicPoint.Current;
        MPtext.text = $"{player_stats.MagicPoint.Current} MP";
    }
}
