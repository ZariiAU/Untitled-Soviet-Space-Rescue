using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Debris Data")]
public class DebrisData : ScriptableObject
{
    public DebrisType debrisType;
    public List<GameObject> models;
    public int value;
    public int damage;
}
