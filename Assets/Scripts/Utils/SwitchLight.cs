using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    [SerializeField] string switchButton = "f";
    private Light lightSource = null;

    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        if(Input.GetKeyDown(switchButton)){
            lightSource.intensity = lightSource.intensity > 0f ? 0f : 7f;
        }
    }
}
