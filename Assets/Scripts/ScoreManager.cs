using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public UnityEvent OnPointAdded;
    public UnityEvent OnPointRemoved;
    public UnityEvent OnPointChanged;
    public int Points { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPoints(int amount)
    {
        Points += amount;
        OnPointAdded.Invoke();
        OnPointChanged.Invoke();
    }
    public void RemovePoints(int amount)
    {
        Points -= amount;
        OnPointRemoved.Invoke();
        OnPointChanged.Invoke();
    }
}
