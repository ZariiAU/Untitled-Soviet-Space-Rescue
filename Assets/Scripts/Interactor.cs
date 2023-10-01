using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    PlayerTracker pm;
    float interactionRange = 5;

    private void Start()
    {
        pm = PlayerTracker.instance;
    }

    void CheckForObject()
    {
        RaycastHit hit;
        Physics.Raycast(pm.playerCam.transform.position, pm.playerCam.transform.forward, out hit, interactionRange);
        Debug.DrawRay(pm.playerCam.transform.position, pm.playerCam.transform.forward, Color.red, 1);

        if (hit.collider)
        {
            if (hit.collider.GetComponent<IInteractable>())
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }

        }
    }
}
