using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarSlotUI : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void SetSlotTextByDebrisType(DebrisType debrisType)
    {
        switch (debrisType)
        {
            case DebrisType.Small:
                text.text = "S";
                break;
            case DebrisType.Medium:
                text.text = "M";
                break;
        }
    }
}
