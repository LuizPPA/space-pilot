using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPositionToObject : MonoBehaviour
{

    [SerializeField] GameObject objectReference = null;
    private bool isFirstPerson = true;
    private Vector3 scale = Vector3.one; 

    // Start is called before the first frame update
    void Start()
    {
        if(objectReference){
            scale = new Vector3(objectReference.transform.lossyScale.x, objectReference.transform.lossyScale.y, objectReference.transform.lossyScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("v")){
            isFirstPerson = !isFirstPerson;
        }
        if(objectReference != null){
            if(isFirstPerson){
                gameObject.transform.position = objectReference.transform.position + GetFirstPersonOffset();
            }
            else{
                gameObject.transform.position = objectReference.transform.position + GetThirdPersonOffset();
            }
            // gameObject.transform.rotation = objectReference.transform.rotation;
        }
    }

    Vector3 GetThirdPersonOffset()
    {
        return (transform.right * -0.8f * scale.x) + (transform.up * 0.6f * scale.y) + (transform.forward * -1.8f * scale.z);
    }

    Vector3 GetFirstPersonOffset()
    {
        return (transform.up * 0.5f * scale.y) + (transform.forward * 0.3f * scale.z);
    }
}
