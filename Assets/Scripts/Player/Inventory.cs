using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent<Debris> OnItemDropped;
    public UnityEvent<Debris> OnItemPickedUp;
    [SerializeField] List<Debris> debrisSlot = new List<Debris>();
    [SerializeField] float pickupRange;
    PlayerTracker pm;
    [SerializeField] Transform debrisDropPoint;
    public Debris selectedItem;
    int carriedItems = 0;
    int maxCarriedItems = 6;

    private void Start()
    {
        ControlHub.Instance.fireInput.AddListener(() => { CheckForObject(); });
        ControlHub.Instance.altFireInput.AddListener(() => { RemoveFromInventory(selectedItem, true); });
        ControlHub.Instance.downScrollInput.AddListener(() => { NextItem(); });
        ControlHub.Instance.upScrollInput.AddListener(() => { PrevItem(); });
        pm = PlayerTracker.instance;
    }

    void NextItem()
    {
        if (debrisSlot.Count > 0)
        {
            if (debrisSlot.IndexOf(selectedItem) < debrisSlot.Count - 1)
                selectedItem = debrisSlot[debrisSlot.IndexOf(selectedItem) + 1];
            else
            {
                selectedItem = debrisSlot[0];
            }
        }
    }
    void PrevItem()
    {   
        if(debrisSlot.Count > 0)
        {
            if (debrisSlot.IndexOf(selectedItem) > 0)
                selectedItem = debrisSlot[debrisSlot.IndexOf(selectedItem) - 1];
            else
            {
                selectedItem = debrisSlot[debrisSlot.Count - 1];
            }
        }
    }

    void CheckForObject()
    {
        RaycastHit hit;
        Physics.Raycast(pm.playerCam.transform.position, pm.playerCam.transform.forward, out hit, pickupRange);
        Debug.DrawRay(pm.playerCam.transform.position, pm.playerCam.transform.forward, Color.red, 1);
        if (hit.collider)
        {
            if (hit.collider.GetComponent<Debris>())
            {
                var debris = hit.collider.GetComponent<Debris>();
                if (debris != null)
                {
                    AddToInventory(debris);
                }
            }
        }
    }

    void AddToInventory(Debris debris)
    {
        if(carriedItems + debris.debrisData.value <= maxCarriedItems)
        {
            if(debris.debrisData.toolRequired == pm.Player.GetComponent<Tool>().data.toolType && debris.debrisData.debrisType != DebrisType.Large 
                || debris.debrisData.debrisType == DebrisType.Small && debris.debrisData.debrisType != DebrisType.Large)
            {
                debrisSlot.Add(debris);
                OnItemPickedUp.Invoke(debris);
                debris.OnPickup.Invoke();
                debris.gameObject.SetActive(false);
                if (carriedItems == 0)
                    selectedItem = debris;
                carriedItems += debris.debrisData.value;
            }
        }
    }

    public void RemoveFromInventory(Debris debris, bool dropInWorld)
    {
        if (debrisSlot.Contains(debris))
        {
            if (dropInWorld) 
            {
                debris.transform.position = debrisDropPoint.position;
                debris.gameObject.SetActive(true);
                debris.GetComponent<Rigidbody>().AddForce(Vector3.forward * 2);
            }
            debrisSlot.Remove(debris);
            carriedItems -= debris.debrisData.value;
            selectedItem = carriedItems == 0 ? null : debrisSlot[0];
            OnItemDropped.Invoke(debris);
        }
    }
}
