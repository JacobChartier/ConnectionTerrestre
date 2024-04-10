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
            BossExists = false;
        }

        enemies.Remove(enemy);
    }

    public void SpawnBoss()
    {
        BossExists = true;
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
