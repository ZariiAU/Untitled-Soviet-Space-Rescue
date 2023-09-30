using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool")]
public class ToolData : ScriptableObject
{
    public ToolType toolType;
    public Mesh toolModel;
}

public enum ToolType
{
    None,
    Net,
    Wrench,
    Explosives
}
