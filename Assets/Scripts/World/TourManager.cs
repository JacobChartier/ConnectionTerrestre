using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TourManager : MonoBehaviour
{
    [SerializeField] private const int NUM_TOURS = 4;

    [SerializeField] private UnityEvent JoueurDansTour;
    private Collider Col;

    public static bool[] ToursCompletes = new bool[4];
    public static int current_tour = -1;

    [SerializeField] private int TOUR_ID = -1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            current_tour = TOUR_ID;
            JoueurDansTour.Invoke();
        }
    }
}
