using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance;
    [SerializeField] GameObject player; 
    [SerializeField] Rigidbody2D playerRB; 
    public GameObject Player { get { return player; } }
    public Rigidbody2D PlayerRB { get { return playerRB; } }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
