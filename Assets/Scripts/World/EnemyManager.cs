using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    [Header("constantes")]
    [SerializeField] private int ENEMY_LIMIT = 4;
    [SerializeField] private float POURCENTAGE_CHANCE_ENEMY_SPAWN = 0.5f;
    [SerializeField] private float DISTANCE_ENEMY_UNLOAD = 100.0f;
    [Space]
    [Header("prefabs")]
    [SerializeField] private GameObject PREFAB_ENEMY;
    [Space]
    [Header("joueur")]
    [SerializeField] private Transform player;
    [Space]
    [Header("Couleure")]
    [SerializeField] public Material material;

    List<Enemy> enemies = new();
    public bool BossExists = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Combat"))
            return;

        if (player == null) // >:(
        {
            player = GameObject.Find("Player").transform;
        }

        // unload les ennemis trop loin du joueur
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, player.position) > DISTANCE_ENEMY_UNLOAD)
            {
                Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (enemies.Count >= ENEMY_LIMIT)
            return;

        if (Random.value * 100 <= POURCENTAGE_CHANCE_ENEMY_SPAWN)
        {
            //Debug.Log("enemy spawn!!!!");
            Vector3 position_enemi = player.position;

            // position joueur + nombre hasard de 100 à 200, positif ou négatif (deux axes)
            position_enemi += Vector3.right * (Random.value * 30 + 20) * (Random.value < 0.5f ? 1 : -1);
            position_enemi += Vector3.forward * (Random.value * 30 + 20) * (Random.value < 0.5f ? 1 : -1);

            if (!Physics.Raycast(position_enemi + Vector3.up * 100, Vector3.down, out RaycastHit r))
                return;

            position_enemi = new Vector3(position_enemi.x, r.point.y + 1, position_enemi.z);

            Enemy e = Instantiate(PREFAB_ENEMY, position_enemi, Quaternion.identity).GetComponent<Enemy>();
            enemies.Add(e);
        }
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        if (enemy.IsBoss())
        {
            Tour.current_tour = -1;
            BossExists = false;
        }

        enemies.Remove(enemy);
    }

    public void SpawnBoss()
    {
        Debug.Log("BOSS!!!");
        if (BossExists)
            return;

        // mets l'ennemi au millieu de la tour
        Tour tour = null;
        GameObject[] tours = GameObject.FindGameObjectsWithTag("Tour");
        foreach (GameObject t in tours)
        {
            if (Vector3.Distance(player.position, t.transform.position) < 100)
            {
                tour = t.GetComponent<Tour>();
                break;
            }
        }
        Vector3 position_enemi = tour.transform.position;

        // position joueur + nombre hasard de 30 à 50, positif ou négatif (deux axes)
        //position_enemi += Vector3.right * (Random.value * 30 + 20) * (Random.value < 0.5f ? 1 : -1);
        //position_enemi += Vector3.forward * (Random.value * 30 + 20) * (Random.value < 0.5f ? 1 : -1);

        Physics.Raycast(position_enemi + Vector3.up * 100, Vector3.down, out RaycastHit r);

        position_enemi = new Vector3(position_enemi.x, r.point.y + 3, position_enemi.z);

        Enemy e = Instantiate(PREFAB_ENEMY, position_enemi, Quaternion.identity).GetComponent<Enemy>();
        enemies.Add(e);
    }
}
