using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerGizmos : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Vector3 rayDirection;
    [SerializeField] private Color colorLookAt = Color.red;

    [SerializeField] private Color colorInteraction = Color.yellow;
    [SerializeField] private float radius = 5;

    [Header("Gizmos")]
    [Tooltip("Draw a ray to see in which direction the player is currently looking at.")]
    [SerializeField] private bool LookAt;
    [Tooltip("Draw a spere to see the area in which the player can interact with objects.")]
    [SerializeField] private bool InteractionRegion;

    [Header("Debug Hacks")]
    [Tooltip("Enter combat with basic enemy when key 'C' pressed")]
    [SerializeField] private bool DEBUG_enter_battle_on_C;

    private void OnDrawGizmos()
    {
        if (LookAt)
        {
            Gizmos.color = colorLookAt;
            Gizmos.DrawRay(new Ray(this.transform.position, new Vector3(this.transform.position.x + this.transform.rotation.y + 10, this.transform.position.y, this.transform.position.z)));
        }

        if (InteractionRegion)
        {
            Gizmos.color = colorInteraction;
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }

    private void FixedUpdate()
    {
        if (DEBUG_enter_battle_on_C)
        {
            if (Input.GetKeyDown(KeyCode.C) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World"))
            {
                SceneManager.LoadScene("Combat");
            }
        }
    }
}

