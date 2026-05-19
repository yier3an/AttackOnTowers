using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public float arrowSpeed = 10.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
