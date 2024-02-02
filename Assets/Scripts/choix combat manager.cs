using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class choixcombatmanager : MonoBehaviour
{
    [SerializeField] Transform lookat;
    [SerializeField] Transform origin;
    [SerializeField] List<GameObject> enfants;
    int timer = 0;
    const float MAGNITUDE = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = origin.position;
    }

    // Update is called once per frame
    void Update()
    {
        float angle_entre_enfants = Mathf.PI * 2 / enfants.Count;
        int index = 0;
        foreach (GameObject o in enfants)
        {
            o.transform.position = new Vector3(
                Mathf.Cos(angle_entre_enfants * index + (timer / 100.0f)) * MAGNITUDE + 10,
                Mathf.Sin(angle_entre_enfants * index + (timer / 100.0f)) * MAGNITUDE,
                0
            );
            index++;
        }

        timer++;
    }
}
