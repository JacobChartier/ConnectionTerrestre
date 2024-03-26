using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextBhv : MonoBehaviour
{
    private TMP_Text tmp;
    [SerializeField] private EntityStats.StatType current_stat;
    [SerializeField] private bool montrer_current_et_max;
    [SerializeField] private string text_divisor = " / ";
    private EntityStats stats_entite;

    // Start is called before the first frame update
    void Start()
    {
        stats_entite = BattleInfo.player;
        tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (montrer_current_et_max)
        {
            switch (current_stat)
            {
                case EntityStats.StatType.HP:
                    tmp.text = ((int)stats_entite.Health.Current).ToString() + text_divisor + ((int)stats_entite.Health.Max).ToString();
                    break;

                case EntityStats.StatType.MP:
                    tmp.text = ((int)stats_entite.MagicPoint.Current).ToString() + text_divisor + ((int)stats_entite.MagicPoint.Max).ToString();
                    break;

                default:
                    return;
            }

            return;
        }

        switch (current_stat)
        {
            case EntityStats.StatType.HP:
                tmp.text = ((int)stats_entite.Health.Current).ToString();
                break;

            case EntityStats.StatType.MP:
                tmp.text = ((int)stats_entite.MagicPoint.Current).ToString();
                break;

            case EntityStats.StatType.DEFENSE:
                tmp.text = ((int)stats_entite.Defense.Current).ToString();
                break;

            case EntityStats.StatType.FORCE:
                tmp.text = ((int)stats_entite.Strength.Current).ToString();
                break;

            case EntityStats.StatType.VITESSE:
                tmp.text = ((int)stats_entite.AttackSpeed.Current).ToString();
                break;

            case EntityStats.StatType.MAX_HP:
                tmp.text = ((int)stats_entite.Health.Max).ToString();
                break;

            case EntityStats.StatType.MAX_MP:
                tmp.text = ((int)stats_entite.MagicPoint.Max).ToString();
                break;

            case EntityStats.StatType.MAX_DEFENSE:
                tmp.text = ((int)stats_entite.Defense.Max).ToString();
                break;

            case EntityStats.StatType.MAX_FORCE:
                tmp.text = ((int)stats_entite.Strength.Max).ToString();
                break;

            case EntityStats.StatType.MAX_VITESSE:
                tmp.text = ((int)stats_entite.AttackSpeed.Max).ToString();
                break;
        }
    }
}
