using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationHealthUI : MonoBehaviour
{
    [SerializeField] Damageable damageable;
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "Station Health: " + damageable.GetHealth();
        damageable.OnDamaged.AddListener(() => { text.text = "Station Health: " + damageable.GetHealth(); });
    }

}
