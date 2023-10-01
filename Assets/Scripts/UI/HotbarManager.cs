using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] HotbarSlotUI slotPrefab;
    Dictionary<Debris, HotbarSlotUI> slots = new Dictionary<Debris, HotbarSlotUI>();

    private void Start()
    {
        inventory = PlayerTracker.instance.Player.GetComponent<Inventory>();
        inventory.OnItemPickedUp.AddListener((debris) => 
        { 
            var slot = Instantiate(slotPrefab, transform); 
            slots.Add(debris, slot);
            slot.GetComponent<HotbarSlotUI>().SetSlotTextByDebrisType(debris.debrisData.debrisType); 
        });
        inventory.OnItemDropped.AddListener((debris) => {

            foreach(var kvp in slots)
            {
                if(kvp.Key == debris)
                {
                    DestroyImmediate(kvp.Value.gameObject);
                    slots.Remove(kvp.Key);
                    break;
                }
            }
        });
    }
}
