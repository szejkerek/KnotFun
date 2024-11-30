using UnityEditor.ShaderGraph;
using UnityEngine;

public class CentralCrystal : MonoBehaviour
{
    Vector3 initialPosition;
    public float maxShift = 0.5f, rotationSpeed = 5, floatSpeed = 0.2f;

    public float colorDelta = 0.05f, colorLoss = 0.02f, maxBrightness = 3;

    public float r = 0, g = 0, b = 0;

    Material crystalMaterial;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;  
        crystalMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * maxShift;
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);

        if (Input.GetKey(KeyCode.R)) r += Time.deltaTime * colorDelta;
        else r -= Time.deltaTime * colorLoss;
        if (Input.GetKey(KeyCode.G)) g += Time.deltaTime * colorDelta;
        else g -= Time.deltaTime * colorLoss;
        if (Input.GetKey(KeyCode.B)) b += Time.deltaTime * colorDelta;
        else b -= Time.deltaTime * colorLoss;
        r = Mathf.Clamp(r, 0, 1);
        g = Mathf.Clamp(g, 0, 1);
        b = Mathf.Clamp(b, 0, 1);


        crystalMaterial.SetColor("_EmissionColor", new Color(r,g,b) * (r+g+b)/3 * maxBrightness);
    }
}
