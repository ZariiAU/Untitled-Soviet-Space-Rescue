using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private ControlHub ch;
    private Rigidbody rb;
    private Vector3 velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float rotationSpeed;
    public bool invertY = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ch = ControlHub.Instance;
            
        // Add inputs to events
        ch.forwardInput.AddListener(() => { velocity += new Vector3(transform.forward.x, transform.forward.y, transform.forward.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });
        ch.backwardInput.AddListener(() => { velocity -= new Vector3(transform.forward.x, transform.forward.y, transform.forward.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });
        ch.leftInput.AddListener(() => { velocity -= new Vector3(transform.right.x, transform.right.y, transform.right.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });
        ch.rightInput.AddListener(() => { velocity += new Vector3(transform.right.x, transform.right.y, transform.right.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });
        ch.upInput.AddListener(() => { velocity += new Vector3(transform.up.x, transform.up.y, transform.up.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });
        ch.downInput.AddListener(() => { velocity -= new Vector3(transform.up.x, transform.up.y, transform.up.z) * acceleration * Time.deltaTime; rb.velocity = velocity; });

    }
    private void Update()
    {
        velocity = rb.velocity;
        if (invertY) 
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(rotationSpeed * Input.GetAxis("Mouse Y"), rotationSpeed * Input.GetAxis("Mouse X"), 0f));
        else
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(-rotationSpeed * Input.GetAxis("Mouse Y"), rotationSpeed * Input.GetAxis("Mouse X"), 0f));
    }
}
