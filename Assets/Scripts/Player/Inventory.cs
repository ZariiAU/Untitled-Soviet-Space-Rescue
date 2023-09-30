using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Debris[] debrisSlot = new Debris[6];
    [SerializeField] float pickupRange;
    PlayerTracker player;
    [SerializeField] Transform debrisDropPoint;
    public Debris selectedItem;

    private void Start()
    {
        ControlHub.Instance.fireInput.AddListener(() => { CheckForObject(); });
        ControlHub.Instance.downScrollInput.AddListener(() => { NextItem(); });
        ControlHub.Instance.upScrollInput.AddListener(() => { PrevItem(); });
        ControlHub.Instance.altFireInput.AddListener(() => { RemoveFromInventory(selectedItem); });
        player = PlayerTracker.instance;
    }

    void NextItem()
    {
        // Initialise our selected object when we first scroll and make sure that the array isn't empty
        if (selectedItem == null && !CheckArrayEmpty(debrisSlot)) 
        {
            selectedItem = debrisSlot[0];
            return;
        }

        // If the inventory is empty, don't try to select anything
        if (CheckArrayEmpty(debrisSlot))
        {
            return;
        }

        int selectedItemIndex = System.Array.IndexOf(debrisSlot, selectedItem);
        if (selectedItemIndex < debrisSlot.Length - 1 && debrisSlot[selectedItemIndex + 1] != null) // Check that we're at least 1 element away from the end of the array and that the next slot isn't empty
        {
            selectedItem = debrisSlot[selectedItemIndex + 1];
        }
        else
        {
            for (int i = selectedItemIndex; i < debrisSlot.Length; i++)
            {
                if (debrisSlot[i] == null && debrisSlot[i + 1])
                {
                    selectedItem = debrisSlot[i + 1]; // select nearest occupied slot
                    break;
                }
                else if (i == debrisSlot.Length - 1)
                {
                    selectedItem = debrisSlot[0]; // If the array is full, then select the first slot.
                }
            }
        }
    }

    void PrevItem()
    {
        // Initialise our selected object when we first scroll and make sure that the array isn't empty
        if (selectedItem == null && !CheckArrayEmpty(debrisSlot)) 
        {
            selectedItem = debrisSlot[0];
            return;
        }

        // If the inventory is empty, don't try to select anything
        if (CheckArrayEmpty(debrisSlot))
        {
            return;
        }

        int selectedItemIndex = System.Array.IndexOf(debrisSlot, selectedItem);
        if (selectedItemIndex > 0 && debrisSlot[selectedItemIndex - 1] != null) // Check that we're at least 1 element away from the start of the array and that the previous slot isn't empty
        {
            selectedItem = debrisSlot[selectedItemIndex - 1];
        }
        else
        {
            for (int i = 0; i < debrisSlot.Length; i++)
            {
                if (debrisSlot[i] == null && debrisSlot[i - 1])
                {
                    selectedItem = debrisSlot[i - 1]; // If we're less than 1 element away (the beginning), then just select furthest occupied slot
                    break;
                }
                else if (i == debrisSlot.Length - 1)
                {
                    selectedItem = debrisSlot[debrisSlot.Length - 1]; // If the array is full, then select the last slot.
                }
            }
        }
    }

    void CheckForObject()
    {
        RaycastHit hit;
        Physics.Raycast(player.playerCam.transform.position, player.playerCam.transform.forward, out hit, pickupRange);
        Debug.DrawRay(player.playerCam.transform.position, player.playerCam.transform.forward, Color.red, 1);
        if (hit.collider)
        {
            var debris = hit.collider.GetComponent<Debris>();
            if (debris != null)
            {
                AddToInventory(debris);
            }
        }
    }

    void AddToInventory(Debris debris)
    {
        for(int i = 0; i < debrisSlot.Length; i++)
        {
            if (debrisSlot[i] != null) // Check if the current slot is full
                continue;

            else if (debris.debrisData.debrisType == DebrisType.Small) // If we're picking up small debris, just add it.
            {
                debrisSlot[i] = debris;
                // TODO: Remove object from world here
                // Disable object
                debris.gameObject.SetActive(false);
                break;
            }
            else if (debris.debrisData.debrisType == DebrisType.Medium && i+1 < debrisSlot.Length) // Mediums take up two slots, we can't add one if theres no space.
            {
                debrisSlot[i] = debris;
                debrisSlot[i + 1] = debris;
                debris.gameObject.SetActive(false);
                break;
            }
        }
    }

    void RemoveFromInventory(Debris debris)
    {
        if (debris)
        {
            for (var i = 0; i < debrisSlot.Length; i++)
            {
                if (debrisSlot[i] == debris && debrisSlot[i].debrisData.debrisType == DebrisType.Medium)
                {
                    // TODO: Spawn the object into the world here
                    // Move referenced object here then enable
                    debrisSlot[i] = null;
                    debrisSlot[i + 1] = null;
                    selectedItem = null;
                    debris.gameObject.transform.position = debrisDropPoint.position;
                    debris.gameObject.SetActive(true);
                }
                else if (debrisSlot[i] == debris && debrisSlot[i].debrisData.debrisType == DebrisType.Small)
                {
                    // TODO: Spawn the object into the world here
                    // Move referenced object here then enable
                    debrisSlot[i] = null;
                    selectedItem = null;
                    debris.gameObject.transform.position = debrisDropPoint.position;
                    debris.gameObject.SetActive(true);
                }
            }
        }
    }

    bool CheckArrayEmpty(Debris[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                return false;
            }
            else continue;
        }
        return true;
    }
}
