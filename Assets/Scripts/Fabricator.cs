using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : IInteractable
{
    public ToolDispenser dispenser;
    int itemCost = 12;

    public void CreateItem(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        dispenser.OnInteracted.AddListener(() => { TakeItem(); });
    }

    public void TakeItem()
    {
        dispenser.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        var inv = PlayerTracker.instance.GetComponent<Inventory>();

        if (ScoreManager.instance.Points >= itemCost)
        {
            ScoreManager.instance.RemovePoints(itemCost);
            CreateItem(dispenser.gameObject);
        }
    }
}
