using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable interactable;
    [SerializeField] float detectionRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("d");
            interactable?.Interact();
        }

        RaycastDetection();
    }

    private void RaycastDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
        {
            IInteractable i = hit.collider.GetComponent<IInteractable>();

            if (i != null)
            {
                interactable = i;
                interactable?.ShowContextLabel();
            }
            else
            {
                interactable?.HideContextLabel();
                interactable = null;
            }
        }
        else
        {
            interactable?.HideContextLabel();
            interactable = null;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * detectionRange);
    }
}
