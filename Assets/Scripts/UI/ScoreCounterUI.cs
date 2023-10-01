using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounterUI : MonoBehaviour
{
    TMP_Text text = null;
    ScoreManager scoreManager;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        scoreManager = ScoreManager.instance;
        scoreManager.OnPointChanged.AddListener(() => { Refresh(); });
    }

    void Refresh()
    {
        text.text = scoreManager.Points.ToString();
    }
}
