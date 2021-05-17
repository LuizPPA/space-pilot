using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [SerializeField] float gravity = 10f;

    private List<GameObject> capturedObjects = new List<GameObject>();
    private float gravityRadius = 1f;
    private Transform planet = null;

    void Start()
    {
        gravityRadius = transform.lossyScale.y / 2f;
    }

    void Update()
    {
        capturedObjects.ForEach(delegate(GameObject obj)
        {
            Atract(obj);
            Orient(obj);
        });
    }

    void Atract(GameObject obj)
    {
        var objRigidbody = obj.GetComponent<Rigidbody>();
        var gravityDirection = (transform.position - obj.transform.position).normalized;
        objRigidbody.AddForce(gravityDirection * GetIntensity(obj) * Time.deltaTime);
    }

    void Orient(GameObject obj)
    {
        var rotationDirection = -(transform.position - obj.transform.position).normalized;
        var targetRotation = Quaternion.FromToRotation(obj.transform.up, rotationDirection) * obj.transform.rotation;
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, GetIntensity(obj) * Time.deltaTime);
    }

    float GetIntensity(GameObject obj)
    {
        var distance = GetDistance(obj);
        return gravity * (1 - (distance / gravityRadius));
    }

    float GetDistance(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, transform.position);
    }

    void OnTriggerEnter(Collider collider)
    {
        var obj = collider.gameObject;
        if(obj.GetComponent<Rigidbody>())
        {
            capturedObjects.Add(collider.gameObject);
            var objectControler = collider.gameObject.GetComponent<PlayerControl>();
            if(objectControler)
            {
                objectControler.SetInGravity(true, planet);
            }
            obj.transform.SetParent(transform.parent);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        var obj = collider.gameObject;
        if(obj.GetComponent<Rigidbody>())
        {
            capturedObjects.Remove(collider.gameObject);
            var objectControler = collider.gameObject.GetComponent<PlayerControl>();
            if(objectControler)
            {
                objectControler.SetInGravity(false, planet);
            }
            obj.transform.SetParent(null);
        }
    }

}
