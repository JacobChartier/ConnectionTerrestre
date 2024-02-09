using UnityEngine;

namespace Assets.Scripts.Combat
{
    // infos pour chaque attaque individuelle
    public struct InfoAttaqueMonstre
    {
        int parry_frame; // nb de frames d'animation avant que l'attaque touche le joueur visuellement
        int block_window; // nb de frames avant parry_frame où que le joueur peut bloquer mais ne va pas parry;

        // id ou objet pour l'animation
        // particules crées

        public InfoAttaqueMonstre(int parry_frame, int block_window)
        {
            if (parry_frame < 0)
                parry_frame = 0;

            this.parry_frame = parry_frame;

            if (block_window < 0)
                block_window = 0;

            this.block_window = block_window;
        }
    }

    // infos pour tous les stats de combat de un monstre spécifique
    [RequireComponent(typeof(EntityStats))]
    internal class InfosMonstre : MonoBehaviour
    {
        EntityStats stats;
        InfoAttaqueMonstre[] attaques;

        private void Awake()
        {
            stats = GetComponent<EntityStats>();
            // TODO: GET MONSTRE ID DE QUELQUEPART QUAND ENCOUNTER
        }
    }
}