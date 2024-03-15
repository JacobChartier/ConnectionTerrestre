using Assets.Scripts.Combat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class selectormanager : MonoBehaviour
{
    public enum StartupType
    {
        PHYSIQUE,
        MAGIQUE,
        ITEM
    }

    [SerializeField] TMP_Text txt_titre;
    [SerializeField] TMP_Text txt_liste;
    [SerializeField] Image img;

    private StartupType type;
    private int selection = 0;
    private List<InfoAttaque> attaques;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        gameObject.SetActive(false);
        attaques = GameObject.Find("Joueur").GetComponent<EntityStats>().Attaques;
        // DEBUG
        attaques.Add(new("test1", 50, 0, 100, 10, true));
        attaques.Add(new("test1.2", 50, 0, 100, 10, true));
        attaques.Add(new("test2", 50, 0, 100, 20, false));
        attaques.Add(new("test3", 50, 0, 100, 100, false));
    }

    public void Startup(StartupType type)
    {
        this.type = type;
        gameObject.SetActive(true);
        selection = 0;

        switch (type)
        {
            case StartupType.PHYSIQUE:
                img.color = Color.red;
                txt_titre.text = "Attaques Physiques";
                break;
            case StartupType.MAGIQUE:
                img.color = Color.yellow;
                txt_titre.text = "Attaques Magiques";
                break;
            case StartupType.ITEM:
                img.color = Color.green;
                txt_titre.text = "Items";
                break;
        }

        txt_liste.text = "";

        if (type == StartupType.ITEM)
            return;

        foreach (InfoAttaque i in attaques)
        {
            if (i.magique == (type == StartupType.MAGIQUE))
            {
                txt_liste.text += $"- {i.nom}\n";
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}