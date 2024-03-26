using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bhvLowHealth : MonoBehaviour
{
    enum Type
    {
        ICONE,
        TEXT
    }

    [SerializeField] private float PERCENTAGE_LOW_HEALTH = 20.0f;
    [SerializeField] private int INTERVALE_BATTEMENTS = 64;
    [SerializeField] private Type effect_type;
    private EntityStats player_stats;
    private TMP_Text tmp;
    private Image img;

    private int timer = 0;
    private int[] battements = { 0, 1, 16, 17 };

    // Start is called before the first frame update
    void Start()
    {
        player_stats = BattleInfo.player;
        tmp = GetComponent<TMP_Text>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player_stats.Health.Current / player_stats.Health.Max * 100.0f > PERCENTAGE_LOW_HEALTH)
        {
            return;
        }

        switch (effect_type)
        {
            case Type.ICONE:
                float taille = 0.5f;
                if (battements.Contains(timer % INTERVALE_BATTEMENTS))
                {
                    taille *= 1.2f;
                }

                transform.localScale = Vector3.one * taille;
                
                break;

            case Type.TEXT:
                float color = Mathf.Abs(Mathf.Sin((float)timer / 8));
                tmp.color = new Color(1, color, color);
                break;
        }

        timer++;
    }
}
