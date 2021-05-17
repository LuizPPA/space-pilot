using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpeed : MonoBehaviour
{

    [SerializeField] float maxSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rigidBd = GetComponent<Rigidbody>();
        if(rigidBd.velocity.magnitude > maxSpeed)
        {
            rigidBd.velocity = rigidBd.velocity.normalized * maxSpeed;
        }
    }
}
