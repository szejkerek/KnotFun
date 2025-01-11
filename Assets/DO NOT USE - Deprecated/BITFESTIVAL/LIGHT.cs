using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class LIGHT : MonoBehaviour
{
    Light lght;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lght = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Period)) lght.intensity += 1;
        else if (Input.GetKeyUp(KeyCode.Comma)) lght.intensity -= 1;
    }
}
