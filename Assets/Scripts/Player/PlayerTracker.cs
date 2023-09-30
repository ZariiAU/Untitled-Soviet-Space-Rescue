using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker instance;
    [SerializeField] GameObject player; 
    [SerializeField] Rigidbody playerRB;
    public Camera playerCam;
    public GameObject Player { get { return player; } }
    public Rigidbody PlayerRB { get { return playerRB; } }

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
}
