using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIten : MonoBehaviour
{

    [SerializeField] GameObject objectReference = null;
    [SerializeField] float lookSpeed = 100f;
    private Vector3 startRotation = Vector3.zero;
    float lookPosition = 0f;

    void Start(){
        startRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if(objectReference != null && objectReference.GetComponent<PlayerControl>()){
            bool inGravity = objectReference.GetComponent<PlayerControl>().isInGravity();
            if(inGravity){
                lookPosition += Input.GetAxis("Mouse Y") * lookSpeed * 2f * Time.deltaTime;
                lookPosition = Mathf.Clamp(lookPosition, -60f, 90f);
                transform.localEulerAngles = (Vector3.left * lookPosition) + startRotation;
            }
            else{
                transform.localEulerAngles = Vector3.zero + startRotation;
            }
        }
    }
}
