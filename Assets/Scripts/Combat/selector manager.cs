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

    Image img;
    [SerializeField] TMP_Text txt_titre;
    [SerializeField] TMP_Text txt_liste;
    [SerializeField] Transform curseur;

    private StartupType type;
    private int selection = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(":)))))");
        img = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Startup(StartupType type)
    {
        this.type = type;
        gameObject.SetActive(true);

        curseur.position = new Vector3(-40, 28, 0);
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

        //todo: code pour lister possibilités
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}