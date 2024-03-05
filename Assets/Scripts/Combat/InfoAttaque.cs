using UnityEngine;

namespace Assets.Scripts.Combat
{
    // infos pour chaque attaque individuelle
    public struct InfoAttaque
    {
        public string nom; // nom de l'attaque
        public int anim_len; // longueure de l'animation en frames.
        public int parry_frame; // nb de frames d'animation avant que l'attaque touche l'opposant visuellement
        public int block_window; // nb de frames avant parry_frame où que le joueur peut bloquer mais ne va pas parry;
        public int damage; // dommages de l'attaque
        public bool magique; // si attaque joueur magique

        // id ou objet pour l'animation
        // particules crées
        // autres effets (camera shake, lumière, etc.)

        public InfoAttaque(string nom, int parry_frame, int block_window, int anim_len, int damage, bool magique = false)
        {
            if (parry_frame < 0)
                parry_frame = 0;

            this.parry_frame = parry_frame;

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