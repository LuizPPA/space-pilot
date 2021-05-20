using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPositionToObject : MonoBehaviour
{

    [SerializeField] GameObject objectReference = null;
    private bool isFirstPerson = true;
    private bool isEnabled = true;
    private float viewDistance = 1.8f;
    private Vector3 scale = Vector3.one;

    void Start()
    {
        if(objectReference)
        {
            scale = new Vector3(objectReference.transform.lossyScale.x, objectReference.transform.lossyScale.y, objectReference.transform.lossyScale.z);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("v"))
        {
            isFirstPerson = !isFirstPerson;
        }
        if(objectReference != null && isEnabled)
        {
            if(isFirstPerson)
            {
                gameObject.transform.position = objectReference.transform.position + GetFirstPersonOffset();
            }
            else
            {
                viewDistance = Mathf.Clamp(viewDistance - Input.mouseScrollDelta.y, 1.8f, 8f);
                gameObject.transform.position = objectReference.transform.position + GetThirdPersonOffset();
            }
        }
    }

    Vector3 GetThirdPersonOffset()
    {
        return (transform.right * -0.8f * scale.x) + (transform.up * 0.6f * scale.y) + (transform.forward * -viewDistance * scale.z);
    }

    Vector3 GetFirstPersonOffset()
    {
        return (transform.up * 0.5f * scale.y) + (transform.forward * 0.3f * scale.z);
    }

    public void SetEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }
}
