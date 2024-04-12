using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hack script pour changer l'apparence de l'ennemi en combat
public class Enemy_combat : MonoBehaviour
{
    [SerializeField] private Material material;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(BattleInfo.enemy.Experience);

        // le matériel devrait déja être correct

        // _SCLERACOLOR est la propriété shader utilisé par le squelette
        //switch (BattleInfo.player.Attaques.Count)
        //{
        //    case 0 or 1 or 2:
        //        break;
        //    case 3 or 4:
        //        material.SetColor("_SCLERACOLOR", Color.yellow);
        //        break;
        //    case 5 or 6:
        //        material.SetColor("_SCLERACOLOR", Color.red);
        //        break;
        //    default:
        //        material.SetColor("_SCLERACOLOR", Color.blue);
        //        break;
        //}


        if (IsBoss())
        {
            Debug.Log("big trouble in little china");
            transform.localScale *= 3;
        }
    }

    public bool IsBoss()
    {
        return BattleInfo.enemy.enemyType >= EnemyType.BOSS1;
    }
}
