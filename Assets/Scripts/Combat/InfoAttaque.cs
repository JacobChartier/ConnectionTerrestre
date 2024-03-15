using UnityEngine;

namespace Assets.Scripts.Combat
{
    // infos pour chaque attaque individuelle
    public struct InfoAttaque
    {
        // nom de l'attaque. en ce moment, ceci est seulement important pour les attaques du joueur
        public string nom;

        // longueure de l'animation en frames. (fixedupdate)
        public int anim_len;

        // numéro de frame d'animation où l'attaquant fait contact avec l'opposant. Ceci détermine quand il faut peser enter pour un crit/parry
        public int contact_frame;

        // nb de frames avant parry_frame où que le joueur peut bloquer partiellement une attaque d'un monstre. ceci est seulement
        // utilisé pour les attaques monstres
        public int block_window;

        // base dommages de l'attaque
        public int damage;

        // si attaque joueur magique. en ce moment, affect seulement les attaques du joueur
        public bool magique;

        // id ou objet pour l'animation
        // particules crées
        // autres effets (camera shake, lumière, etc.)

        public InfoAttaque(string nom, int parry_frame, int block_window, int anim_len, int damage, bool magique = false)
        {
            if (parry_frame < 0)
                parry_frame = 0;

            this.contact_frame = parry_frame;

            if (block_window < 0)
                block_window = 0;

            this.block_window = block_window;

            if (anim_len < 0)
                anim_len = 0;

            this.anim_len = anim_len;
            this.damage = damage;
            this.nom = nom;
            this.magique = magique;
        }
    }

    // infos pour tous les stats de combat de un monstre spécifique
    // REMPLACÉ par une liste d'attaques dans entitystats
    //[RequireComponent(typeof(EntityStats))]
    //internal class InfosMonstre : MonoBehaviour
    //{
    //    public EntityStats stats;
    //    public InfoAttaque[] attaques;

    //    private void Awake()
    //    {
    //        stats = GetComponent<EntityStats>();
    //        // TODO: GET MONSTRE ID DE QUELQUEPART QUAND ENCOUNTER
    //    }

        
    //}
}