using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositPoint : IInteractable
{
    public override void Interact()
    {
        var inv = PlayerTracker.instance.Player.GetComponent<Inventory>();
        if (inv.selectedItem)
        {
            ScoreManager.instance.AddPoints(inv.selectedItem.debrisData.value);
            inv.RemoveFromInventory(inv.selectedItem, false);
        }
    }
}
