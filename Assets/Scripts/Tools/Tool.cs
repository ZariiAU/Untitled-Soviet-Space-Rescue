using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolData data;
    PlayerTracker pm;
    float pickupRange = 5;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject ghostExplosivePrefab;
    [SerializeField] GameObject explosivePrefab;
    [SerializeField] Transform toolHoldLocation;
    [SerializeField] GameObject heldToolVisual;

    private void Start()
    {
        pm = PlayerTracker.instance;
        ControlHub.Instance.fireInput.AddListener(() => { CheckForObject(); });
        ghost = Instantiate(ghostExplosivePrefab, transform.position, Quaternion.identity);
        ghost.SetActive(false);
        heldToolVisual = Instantiate(data.toolModel, toolHoldLocation, false);
        heldToolVisual.transform.position = toolHoldLocation.position;
    }

    private void Update()
    {
        if (heldToolVisual != data.toolModel)
        {
            Destroy(heldToolVisual);
            heldToolVisual = data.toolModel;
            heldToolVisual = Instantiate(data.toolModel, toolHoldLocation, false);
            heldToolVisual.transform.position = toolHoldLocation.position;
        }

        // Draw Explosive Ghost
        if(data.toolType == ToolType.Explosives)
        {
            RaycastHit hit;
            if(Physics.Raycast(pm.playerCam.transform.position, pm.playerCam.transform.forward, out hit, pickupRange) )
            {
                if(hit.collider.GetComponent<Debris>()?.debrisData.debrisType == DebrisType.Large)
                {
                    ghost.SetActive(true);
                    ghost.transform.position = hit.point;
                    ghost.transform.rotation = Quaternion.LookRotation(hit.normal);
                }
            }
            else
            {
                ghost?.SetActive(false);
            }
        }
    }

    void CheckForObject()
    {
        RaycastHit hit;
        Physics.Raycast(pm.playerCam.transform.position, pm.playerCam.transform.forward, out hit, pickupRange);
        Debug.DrawRay(pm.playerCam.transform.position, pm.playerCam.transform.forward, Color.green, 1);

        // Handle if we are looking at the tool dispenser
        if (hit.collider)
        {
            if (hit.collider.GetComponent<ToolDispenser>())
            {
                var tool = hit.collider.GetComponent<ToolDispenser>();
                if (tool != null)
                {
                    tool.OnInteracted.Invoke();
                    data = tool.toolData;
                    ghost = Instantiate(data.toolModel, transform.position, Quaternion.identity);
                    ghost.SetActive(false);
                }
            }

            // If we're looking at a large debris and have an explosive tool
            else if(hit.collider.GetComponent<Debris>() && hit.collider.GetComponent<Debris>().debrisData.debrisType == DebrisType.Large && data.toolType == ToolType.Explosives)
            {
                var ex = Instantiate(explosivePrefab,hit.point, Quaternion.LookRotation(hit.normal));
                ex.GetComponent<Explosive>().attachedObject = hit.collider.gameObject;
                ex.transform.parent = hit.collider.transform;
                data = Resources.Load("None") as ToolData;
                ghost.SetActive(false);
            }

            else if (hit.collider.GetComponent<IInteractable>())
            {
                hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
    }
}

