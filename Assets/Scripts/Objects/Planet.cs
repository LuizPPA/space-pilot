using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] GameObject orbitBody = null;
    [SerializeField] bool rotate = true;
    [SerializeField] bool translate = true;
    [SerializeField] float translationSpeed = 1f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Vector3 rotationAngle = Vector3.right;

    void FixedUpdate()
    {
        if(rotate) PerformRotate();
        if(translate) PerformTranslate();
    }

    void PerformTranslate()
    {
        if(orbitBody != null)
        {
            transform.RotateAround(orbitBody.transform.position, Vector3.right, translationSpeed * Time.fixedDeltaTime);
        }
    }

    void PerformRotate()
    {
        if(orbitBody != null)
        {
            transform.Rotate(rotationAngle.normalized * rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
