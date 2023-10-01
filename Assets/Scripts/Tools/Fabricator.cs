using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : IInteractable
{
    public ToolDispenser dispenserPrefab;
    ToolDispenser currentDispensedItem;
    [SerializeField] Transform dispenseLocation;
    [SerializeField] int itemCost = 12;

    public void CreateItem(GameObject gameObject)
    {
        currentDispensedItem = Instantiate(dispenserPrefab, dispenseLocation.position, Quaternion.LookRotation(transform.forward));
        currentDispensedItem.OnInteracted.AddListener(() => {
            TakeItem();
        });
    }

    private void Start()
    {
        
    }

    public void TakeItem()
    {
        Destroy(currentDispensedItem.gameObject);
    }

    public override void Interact()
    {
        var inv = PlayerTracker.instance.GetComponent<Inventory>();

        if (ScoreManager.instance.Points >= itemCost && currentDispensedItem == null)
        {
            ScoreManager.instance.RemovePoints(itemCost);
            CreateItem(dispenserPrefab.gameObject);
        }
    }
    public void Interact(bool removePoints)
    {
        var inv = PlayerTracker.instance.GetComponent<Inventory>();

        if (removePoints)
        {
            if (ScoreManager.instance.Points >= itemCost && currentDispensedItem == null)
            {

                ScoreManager.instance.RemovePoints(itemCost);
                CreateItem(dispenserPrefab.gameObject);
            }
        }
        else
        {
            CreateItem(dispenserPrefab.gameObject);
        }
    }
}
