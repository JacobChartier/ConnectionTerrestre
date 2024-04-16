using Assets.Scripts.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    DEFAULT,
    JAUNE,
    ROUGE,
    BLEU,
    BOSS1,
    BOSS2,
    BOSS3,
    BOSS4
}

public class Enemy : MonoBehaviour
{
    // F U C K   I T   W E   B A L L
    private List<EntityStats_struct> DATA_LIST = new(Enum.GetValues(typeof(EnemyType)).Length)
    {
        new EntityStats_struct(20, 0, 2, 2, 10, 30, 0, // MONSTRE DEFAULT
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 5),
                new InfoAttaque(30, 20, 80, 6),
            }
        ),
        new EntityStats_struct(30, 0, 4, 4, 20, 100, 0, // MONSTRE JAUNE
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 10),
                new InfoAttaque(30, 20, 80, 15),
            }
        ),
        new EntityStats_struct(50, 0, 6, 6, 30, 200, 0, // MONSTRE ROUGE
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 25),
                new InfoAttaque(30, 20, 80, 28),
            }
        ),
        new EntityStats_struct(80, 0, 10, 10, 50, 400, 0, // MONSTRE BLEU
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 30),
                new InfoAttaque(30, 20, 80, 40),
            }
        ),

        new EntityStats_struct(50, 0, 5, 5, 30, 500, 0, // BOSS 1
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 12),
                new InfoAttaque(30, 20, 80, 15),
                new InfoAttaque(30, 20, 80, 18),
            }
        ),
        new EntityStats_struct(80, 0, 8, 8, 50, 1000, 0, // BOSS 2
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 20),
                new InfoAttaque(30, 20, 80, 25),
                new InfoAttaque(30, 20, 40, 25),
            }
        ),
        new EntityStats_struct(100, 0, 12, 12, 70, 2000, 0, // BOSS 3
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 35),
                new InfoAttaque(30, 20, 80, 40),
                new InfoAttaque(30, 20, 80, 50),
            }
        ),
        new EntityStats_struct(200, 0, 15, 15, 200, 1, 0, // BOSS 4
            new List<InfoAttaque>()
            {
                new InfoAttaque(30, 30, 100, 60),
                new InfoAttaque(30, 20, 80, 70),
                new InfoAttaque(30, 20, 80, 80),
                new InfoAttaque(50, 50, 80, 100),
            }
        ),
    };

    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private string text;
    [SerializeField] private CapsuleCollider CC;

    [Header("Info pour combat")]
    [SerializeField] private EntityStats joueur;
    [SerializeField] private Inventory inventaire;

    private EnemyManager EM;
    //private SpriteRenderer SR;
    [SerializeField] private static Material material;
    public EnemyType type = default;

    private void Awake()
    {
        EM = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        material = EM.material;
        //SR = GetComponent<SpriteRenderer>();
        CC = GetComponent<CapsuleCollider>();

        // ??? serializefield ne fonctionne pas correctement dans les prefabs et je ne sais pas pourquoi...
        joueur = GameObject.Find("Player").GetComponent<EntityStats>();
        inventaire = GameObject.Find("Player").GetComponentInChildren<Inventory>();

        if (DATA_LIST.Count != Enum.GetValues(typeof(EnemyType)).Length)
        {
            Destroy(this);
            throw new InvalidProgramException("DATA_LIST doit contenir autant d'information qu'il y a de types ennemis.");
        }

        if (BattleInfo.player == null)
        {
            BattleInfo.player = joueur;
        }

        if (BattleInfo.inventory == null)
        {
            BattleInfo.inventory = inventaire;
            DontDestroyOnLoad(inventaire);
        }
    }

    private void Start()
    {
        // _SCLERACOLOR est la propriété shader utilisé par le squelette
        switch (BattleInfo.player.Attaques.Count)
        {
            case 0 or 1 or 2:
                type = EnemyType.DEFAULT;
                material.SetColor("_SCLERACOLOR", Color.white);
                break;
            case 3 or 4:
                type = EnemyType.JAUNE;
                material.SetColor("_SCLERACOLOR", Color.yellow);

                break;
            case 5 or 6:
                type = EnemyType.ROUGE;
                material.SetColor("_SCLERACOLOR", Color.red);
                break;
            default:
                type = EnemyType.BLEU;
                material.SetColor("_SCLERACOLOR", Color.blue);
                break;
        }

        if (!EM.BossExists && Tour.current_tour >= 0)
        {
            EM.BossExists = true;
            type += (int)EnemyType.BOSS1;
        }

        if (IsBoss())
        {
            Debug.Log("big trouble in little china");
            transform.localScale *= 3;
        }

        GetComponent<EntityStats>().SetStats(DATA_LIST[(int)type]);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name != "Player collision")
            return;

        CombatCommence();
    }

    public void CombatCommence()
    {
        //GameManager.Instance.playerES.Health = GameObject.Find("Player").GetComponent<EntityStats>().Health;
        //GameManager.Instance.enemyES = GetComponent<EntityStats>();
        //InventoryLoader.Save(Player.Instance.inventory);
        //InventoryLoader.Save(Player.Instance.inventory);
        GetComponent<EntityStats>().enemyType = type;

        //Debug.Log(BattleInfo.enemy.Experience + "c");
        BattleInfo.enemy = GetComponent<EntityStats>();
        Debug.Log(BattleInfo.enemy.Experience + "d");

        BattleInfo.inventory = inventaire;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }

    //public override void ShowContextLabel()
    //{
    //    ContextLabelUI.Instance.ShowContextLabel("E", "Enter Combat");
    //}

    private void OnDestroy()
    {
        if (type >= EnemyType.BOSS1)
        {
            EM.BossExists = false;
        }

        EM.RemoveEnemyFromList(this);
    }

    public bool IsBoss()
    {
        return type >= EnemyType.BOSS1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawSphere(CC.center + transform.position, CC.radius);
        Gizmos.DrawLine(transform.position, GameObject.Find("Player").transform.position);
    }
}
