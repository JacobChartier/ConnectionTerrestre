using UnityEngine;

namespace Assets.Scripts.Combat
{
    // infos pour chaque attaque individuelle
    public struct InfoAttaqueMonstre
    {
        public float anim_len; // longueure de l'animation en secondes.
        public int parry_frame; // nb de frames d'animation avant que l'attaque touche le joueur visuellement
        public int block_window; // nb de frames avant parry_frame où que le joueur peut bloquer mais ne va pas parry;
        public int damage; // dommages de l'attaque

        // id ou objet pour l'animation
        // particules crées
        // autres effets (camera shake, lumière, etc.)

        public InfoAttaqueMonstre(int parry_frame, int block_window, float anim_len, int damage)
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
        }
    }

    // infos pour tous les stats de combat de un monstre spécifique
    [RequireComponent(typeof(EntityStats))]
    internal class InfosMonstre : MonoBehaviour
    {
        public EntityStats stats;
        public InfoAttaqueMonstre[] attaques;

        private void Awake()
        {
            stats = GetComponent<EntityStats>();
            // TODO: GET MONSTRE ID DE QUELQUEPART QUAND ENCOUNTER
        }

        
    }
}