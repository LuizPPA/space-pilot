using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWithForce : MonoBehaviour
{
    [SerializeField] Vector3 forceDirection = new Vector3(0, 0, 0);
    [SerializeField] float intensity = 1f;

    void Start()
    {
        var rigdBd = GetComponent<Rigidbody>();
        rigdBd.AddForce(forceDirection * intensity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
