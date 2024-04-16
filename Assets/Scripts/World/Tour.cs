using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tour : MonoBehaviour
{
    private static Dictionary<int, Tour> instances = new();

    [SerializeField] private const int NUM_TOURS = 4;

    [SerializeField] private UnityEvent JoueurDansTour;
    private Collider Col;

    public static bool[] ToursCompletes = new bool[4];
    public static int current_tour = -1;

    [SerializeField] private int TOUR_ID = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (!instances.TryGetValue(TOUR_ID, out Tour _))
        {
            DontDestroyOnLoad(gameObject);
            instances.Add(TOUR_ID, this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ToursCompletes[TOUR_ID])
            return;

        if (other.name == "Player collision")
        {
            current_tour = TOUR_ID;
            JoueurDansTour.Invoke();
        }
    }
}
