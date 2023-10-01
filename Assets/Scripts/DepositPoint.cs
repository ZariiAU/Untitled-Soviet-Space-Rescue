using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DepositPoint : IInteractable
{
    public UnityEvent OnDeposit;
    public override void Interact()
    {
        var inv = PlayerTracker.instance.Player.GetComponent<Inventory>();
        if (inv.selectedItem)
        {
            ScoreManager.instance.AddPoints(inv.selectedItem.debrisData.value);
            var temp = inv.selectedItem;
            inv.RemoveFromInventory(inv.selectedItem, false);
            Destroy(temp.gameObject);
            OnDeposit.Invoke();
        }
    }
}
