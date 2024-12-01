using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CentralCrystal : MonoBehaviour
{
    public static Action onWin;
    Vector3 initialPosition;
    public float maxShift = 0.5f, rotationSpeed = 5, floatSpeed = 0.2f;

    public float colorDelta = 0.05f, colorLoss = 0.02f, maxBrightness = 3;

    public float r = 0, g = 0, b = 0;

    public Material crystalMaterial;

    public bool activated = false;
    public List<EndGamePad> engamePads = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initialPosition = transform.position;  
        crystalMaterial = GetComponent<MeshRenderer>().material;

        foreach (var pad in engamePads)
        {
            pad.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * maxShift;
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
        
        if (activated)
        {
            if (engamePads.All(pad => pad.winnerIsStanding))
            {
                Debug.Log("All pads are winning!");
                //Load scene
            }
            return;
        }

        r -= Time.deltaTime * colorLoss;
        g -= Time.deltaTime * colorLoss;
        b -= Time.deltaTime * colorLoss;

        r = Mathf.Clamp(r, 0, 1);
        g = Mathf.Clamp(g, 0, 1);
        b = Mathf.Clamp(b, 0, 1);


        crystalMaterial.SetColor("_EmissionColor", new Color(r,g,b) * (r+g+b)/3 * maxBrightness);
    }

    public void AddColor(Color col)
    {
        r += Time.deltaTime * colorDelta * Mathf.Clamp(col.r, 0, 1);
        g += Time.deltaTime * colorDelta * Mathf.Clamp(col.g, 0, 1);
        b += Time.deltaTime * colorDelta * Mathf.Clamp(col.b, 0, 1);

        if (r + g + b > 2.8f) Activate();
    }

    public void Activate()
    {
        r = 1;
        g = 1;
        b = 1;

        activated = true;
        foreach (var pad in engamePads)
        {
            pad.gameObject.SetActive(true);
        }
        onWin?.Invoke();
    }
}
