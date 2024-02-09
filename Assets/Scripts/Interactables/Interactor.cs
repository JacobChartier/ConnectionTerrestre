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
            interactable?.Interact();
        }
    }

    private void RaycastDetection()
    {
        Ray ray = new Ray(this.transform.position, Camera.main.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, Camera.main.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable i = other.GetComponent<IInteractable>();

        if (i != null)
        {
            interactable = i;
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
