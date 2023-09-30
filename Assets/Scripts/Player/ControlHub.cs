using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlHub : MonoBehaviour
{
    public static ControlHub Instance { get; private set; }

    public UnityEvent forwardInput;
    public UnityEvent backwardInput;
    public UnityEvent leftInput;
    public UnityEvent rightInput;
    public UnityEvent upInput;
    public UnityEvent downInput;
    public UnityEvent fireInput;
    public UnityEvent altFireInput;
    public UnityEvent oneInput;
    public UnityEvent twoInput;
    public UnityEvent threeInput;
    public UnityEvent fourInput;
    public UnityEvent fiveInput;
    public UnityEvent sixInput;

    public UnityEvent forwardReleasedInput;
    public UnityEvent backwardReleasedInput;
    public UnityEvent leftReleasedInput;
    public UnityEvent rightReleasedInput;
    public UnityEvent upReleasedInput;
    public UnityEvent downReleasedInput;

    public UnityEvent upScrollInput;
    public UnityEvent downScrollInput;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            forwardInput.Invoke();
        }
        if (Input.GetKey(KeyCode.S))
        {
            backwardInput.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightInput.Invoke();
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            upInput.Invoke();
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            downInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            altFireInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forwardReleasedInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backwardReleasedInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightReleasedInput.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftReleasedInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            oneInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            twoInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            threeInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            fourInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            fiveInput.Invoke();
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            sixInput.Invoke();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            upScrollInput.Invoke();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            downScrollInput.Invoke();
        }
    }
}
