using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolDispenser : MonoBehaviour
{
    public ToolData toolData;
    public UnityEvent OnInteracted;
    Fabricator fabricator;

    private void Start()
    {
        fabricator = FindObjectOfType<Fabricator>();
        OnInteracted.AddListener(() => 
        { 
            if (PlayerTracker.instance.Player.GetComponent<Tool>().data.toolType == ToolType.Explosives)
            {
                fabricator.Interact(false);
            }
        });
    }


}
